﻿using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace LinenSys
{
    class Orders
    {
        private int orderID;
        private String orderDate;
        private String deliveryDate;
        private char orderStatus;
        private char orderType;
        private int customerID;

        public Orders()
        {
            setOrderID(000000);
            setOrderDate("00/00/0000");
            setDeliveryDate("00/00/0000");
            setOrderStatus('N');
            setOrderType('N');
            setCustomerID(000000);
        }

        public Orders(int orderID, String orderDate, String deliveryDate, char orderStatus, char orderType, int customerID)
        {
            setOrderID(orderID);
            setOrderDate(orderDate);
            setDeliveryDate(deliveryDate);
            setOrderStatus(orderStatus);
            setOrderType(orderType);
            setCustomerID(customerID);
        }

        private void setOrderID(int orderID)
        {
            this.orderID = orderID;
        }

        private void setOrderDate(string orderDate)
        {
            this.orderDate = orderDate;
        }

        private void setDeliveryDate(string deliveryDate)
        {
            this.deliveryDate = deliveryDate;
        }

        private void setOrderStatus(char orderStatus)
        {
            this.orderStatus = orderStatus;
        }

        private void setOrderType(char orderType)
        {
            this.orderType = orderType;
        }

        private void setCustomerID(int customerID)
        {
            this.customerID = customerID;
        }

        public static DataSet getOrders(DataSet ds)
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);

            String strSQL = "SELECT * FROM Orders ORDER BY Order_ID";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.Fill(ds, "ss");

            conn.Close();

            return ds;
        }

        public static DataSet getOrders(DataSet ds, String SOrder)
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);

            String strSQL = "SELECT * FROM Orders ORDER BY " + SOrder;
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.Fill(ds, "ss");

            conn.Close();

            return ds;
        }

        public static Boolean alreadyExists(string Code)
        {
            Boolean answer = false;

            OracleConnection conn = new OracleConnection(DBConnect.oradb);
            conn.Open();

            String strSQL = "SELECT * FROM Orders WHERE Order_ID = '" + Code + "'";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            OracleDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                answer = true;
            }

            conn.Close();
            return answer;
        }

        public static DataTable getMatchingNames(DataTable ds, String code)
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);

            String strSQL = "SELECT * FROM Orders WHERE Order_ID LIKE '%" + code + "%'";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.Fill(ds);

            conn.Close();

            return ds;
        }

        public static int getNextOrderID()
        {
            int nextOrderID;

            OracleConnection conn = new OracleConnection(DBConnect.oradb);
            conn.Open();

            String strSQL = "SELECT MAX(Order_ID) FROM Orders";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            OracleDataReader dr = cmd.ExecuteReader();

            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextOrderID = 1;
            }
            else
            {
                nextOrderID = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            conn.Close();

            return nextOrderID;
        }

        /*public void regLinen()
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);
            conn.Open();

            String strSQL = "INSERT INTO Linen VALUES('" + this.customerID.ToString() +
                "','" + this.companyName.ToString() + "'," + this.contactNo + "," + this.customerName +
                "," + this.email + "," + this.street + ",'" + this.customerStatus.ToString() + "')";

            OracleCommand cmd = new OracleCommand(strSQL, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void updateLinen()
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);
            conn.Open();

            String strSQL = "UPDATE Linen SET Linen_Name = '" + this.companyName.ToString() + "', Hire_Price = " +
                this.contactNo + ", Cleaning_Price = " + this.customerName + ", Reject_Price = " +
                this.email + ", Pack_Size = " + this.street + " WHERE Linen_Code = '" + this.customerID.ToString() + "'";

            OracleCommand cmd = new OracleCommand(strSQL, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void removeLinen()
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);
            conn.Open();

            String strSQL = "UPDATE Linen SET Linen_Status = 'I' WHERE Linen_Code = '" + this.customerID.ToString() + "'";

            OracleCommand cmd = new OracleCommand(strSQL, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }*/
    }
}
