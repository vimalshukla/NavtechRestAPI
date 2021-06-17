using Database;
using Models.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Services.Helper;

namespace Services.Order
{
    public class OrderService : IOrderService
    {
        #region Properties and Constructor
        readonly SimpleStoreEntities _db;
        readonly string dbConnStr;
        DataBaseOperations dbHelper;
        public OrderService()
        {
            this._db = new SimpleStoreEntities();
            dbHelper = new DataBaseOperations();
            dbConnStr  = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        #endregion

        public List<OrderDetailsAPIModel> GetOrders(int pageIndex, int pageSize, string orderBy = ApiConstants.OrderByID, string direction = ApiConstants.SortAscending)
        {
            List<OrderDetailsAPIModel> orderDetails = new List<OrderDetailsAPIModel>();
            if (pageIndex > 0 && pageSize > 0)
            {
                using (SqlConnection connection = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlcom = new SqlCommand(ApiConstants.P_GetOrderList, connection);
                    sqlcom.Parameters.Add(dbHelper.SetParameter("Size", pageSize, SqlDbType.Int, ParameterDirection.Input));
                    sqlcom.Parameters.Add(dbHelper.SetParameter("Page", pageIndex - 1, SqlDbType.Int, ParameterDirection.Input));
                    sqlcom.Parameters.Add(dbHelper.SetParameter("Orderby", orderBy, SqlDbType.NVarChar, ParameterDirection.Input));
                    sqlcom.Parameters.Add(dbHelper.SetParameter("Direction", direction, SqlDbType.NVarChar, ParameterDirection.Input));

                    DataSet ds = dbHelper.ExecuteStoredProcedure(connection, sqlcom);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count >= 1)
                        {
                            orderDetails = ModelMapper.ReadData<OrderDetailsAPIModel>(ds.Tables[0]);
                        }
                    }
                }
            }
            else
            {
                throw new Exception(ApiConstants.InvalidPageInput);
            }

            return orderDetails;
        }

        public CreateOrderData CreateOrder(CreateOrderData orderDetails)
        {
            DataTable dt = CreateOrderedPrductsDT(orderDetails.Products);

            using (SqlConnection connection = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlcom = new SqlCommand("P_CreateOrderWithProductDetails", connection);
                sqlcom.Parameters.Add(dbHelper.SetParameter("@OrderProductsMapping", dt, SqlDbType.Structured, ParameterDirection.Input));
                sqlcom.Parameters.Add(dbHelper.SetParameter("CustomerId", orderDetails.CustomerId, SqlDbType.Int, ParameterDirection.Input));
                sqlcom.Parameters.Add(dbHelper.SetParameter("OrderStatusId", orderDetails.OrderStatusId, SqlDbType.Int, ParameterDirection.Input));
                sqlcom.Parameters.Add(dbHelper.SetParameter("TotalAmount", orderDetails.TotalAmount, SqlDbType.Float, ParameterDirection.Input));
                sqlcom.Parameters.Add(dbHelper.SetParameter("PaymentModeId", orderDetails.PaymentModeId, SqlDbType.Int, ParameterDirection.Input));

                DataSet ds = dbHelper.ExecuteStoredProcedure(connection, sqlcom);

                if (!(bool)ds.Tables[0].Rows[0]["Result"])
                {
                    throw new Exception(Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]));
                }
            }

            return orderDetails;
        }

        private static DataTable CreateOrderedPrductsDT(List<Products> orderedProducts)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("OrderId", typeof(int)));
            dt.Columns.Add(new DataColumn("ProductId", typeof(int)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(int)));
            foreach (Products order in orderedProducts)
            {
                dt.Rows.Add(0, order.ProductId, order.Quantity);
            }
            return dt;
        }    
        
    }    
}

