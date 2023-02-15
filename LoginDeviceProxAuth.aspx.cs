using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechStore
{
    public partial class LoginDeviceProxAuth : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCheckDeviceAddress_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            Int32 UserID;
            //Check username in db and Get UserID
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT UserID FROM Users WHERE Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", username);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                UserID = Convert.ToInt32(dt.Rows[0][0].ToString());
                //Check if User has enabled Device Prox Auth
                SqlCommand cmd1 = new SqlCommand("SELECT DeviceAddress FROM BluetoothDeviceAddress WHERE UserID=@UserID", con);
                cmd1.Parameters.AddWithValue("@UserID", UserID);
                DataTable dt2 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd1);
                sda2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    //Check if IRL CONNECTED Device Address matches Device Address in DB
                    string StoredDeviceAddress = dt2.Rows[0][0].ToString();
                    BluetoothClient client = new BluetoothClient();
                    BluetoothDeviceInfo[] pairedDevices = client.DiscoverDevices(255, false, true, false);
                    BluetoothDeviceInfo device = null;
                    if (pairedDevices.Length > 0)
                    {
                        foreach (BluetoothDeviceInfo pairedDevice in pairedDevices)
                        {
                            if (pairedDevice.Connected)
                            {
                                device = pairedDevice;
                                System.Diagnostics.Debug.WriteLine(device.DeviceName);
                                System.Diagnostics.Debug.WriteLine(device.DeviceAddress);
                                System.Diagnostics.Debug.WriteLine("Device connected: " + device.Connected);
                                System.Diagnostics.Debug.WriteLine("-----------------------------------------");
                                if (device.DeviceAddress.ToString() == StoredDeviceAddress)
                                {
                                    try
                                    {
                                        lblMsg.Text = "";
                                        System.Diagnostics.Debug.WriteLine(device.DeviceName);
                                        System.Diagnostics.Debug.WriteLine(device.DeviceAddress.ToString());
                                        System.Diagnostics.Debug.WriteLine("Here");
                                        txtDeviceAddress.Attributes["value"] = device.DeviceAddress.ToString();
                                        client.Connect(device.DeviceAddress, BluetoothService.SerialPort);

                                        byte[] message = System.Text.Encoding.ASCII.GetBytes("Hello Bluetooth");
                                        client.GetStream().Write(message, 0, message.Length);
                                        lblMsg.Text = "";
                                        SqlCommand cmd2 = new SqlCommand("SELECT UserID FROM Users WHERE Username=@Username", con);
                                        cmd2.Parameters.AddWithValue("@Username", username);
                                        DataTable dt3 = new DataTable();
                                        SqlDataAdapter sda3 = new SqlDataAdapter(cmd);
                                        sda.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            UserID = Convert.ToInt32(dt.Rows[0][0].ToString());
                                            Session["LoginID"] = UserID;
                                            Response.Redirect("Login_2FA");
                                        }
                                        else
                                        {
                                            lblMsg.Text = "An Error Has Occurred";
                                        }
                                    }
                                    catch
                                    {
                                        lblMsg.Text = "";
                                        SqlCommand cmd2 = new SqlCommand("SELECT UserID FROM Users WHERE Username=@Username", con);
                                        cmd2.Parameters.AddWithValue("@Username", username);
                                        DataTable dt3 = new DataTable();
                                        SqlDataAdapter sda3 = new SqlDataAdapter(cmd);
                                        sda.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            UserID = Convert.ToInt32(dt.Rows[0][0].ToString());
                                            Session["LoginID"] = UserID;
                                            Response.Redirect("Login_2FA");
                                        }
                                        else
                                        {
                                            lblMsg.Text = "An Error Has Occurred";
                                        }
                                    }
                                }
                                else
                                {
                                    txtDeviceAddress.Attributes["value"] = "";
                                    lblMsg.Text = "Ensure The Correct Device Is Connected";
                                    System.Diagnostics.Debug.WriteLine("Wrong Connected Device");
                                }
                            }
                            else
                            {
                                txtDeviceAddress.Attributes["value"] = "";
                                lblMsg.Text = "Ensure Paired Device Is Connected";
                                System.Diagnostics.Debug.WriteLine("Paired Device Not Connected");
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "No Paired Devices Found";
                        System.Diagnostics.Debug.WriteLine("No phone paired");
                    }
                    client.Close();
                }
                else
                {
                    lblMsg.Text = "This account does not have Device Proximity Auth enabled.";
                }

            }
            else
            {
                lblMsg.Text = "This account does not exist. Please check the entered username";
            }
        }
    }
}