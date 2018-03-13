﻿using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace LinenSys
{
    class Rejects
    {
        private int rejectID;
        private String rejectDate;
        private int rejectQty;
        private String linenCode;
        private int orderID;

        public Rejects()
        {
            setRejectID(000000);
            setRejectDate("00/00/0000");
            setRejectQty(0);
            setLinenCode("NA");
            setOrderID(000000);
        }

        public Rejects(int rejectID, String rejectDate, int rejectQty, String linenCode, int OrderID)
        {
            setRejectID(rejectID);
            setRejectDate(rejectDate);
            setRejectQty(rejectQty);
            setLinenCode(linenCode);
            setOrderID(orderID);
        }

        private void setRejectID(int rejectID)
        {
            this.rejectID = rejectID;
        }

        private void setRejectDate(string rejectDate)
        {
            this.rejectDate = rejectDate;
        }

        private void setRejectQty(int rejectQty)
        {
            this.rejectQty = rejectQty;
        }

        private void setLinenCode(string linenCode)
        {
            this.linenCode = linenCode;
        }

        private void setOrderID(int orderID)
        {
            this.orderID = orderID;
        }

        public static DataSet getRejects(DataSet ds)
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);

            String strSQL = "SELECT * FROM Rejects ORDER BY Reject_ID";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.Fill(ds, "ss");

            conn.Close();

            return ds;
        }

        public static DataSet getRejects(DataSet ds, String SOrder)
        {
            OracleConnection conn = new OracleConnection(DBConnect.oradb);

            String strSQL = "SELECT * FROM Rejects ORDER BY " + SOrder;
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

            String strSQL = "SELECT * FROM Rejects WHERE Reject_ID = '" + Code + "'";
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

            String strSQL = "SELECT * FROM Rejects WHERE Reject_ID LIKE '%" + code + "%'";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.Fill(ds);

            conn.Close();

            return ds;
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
