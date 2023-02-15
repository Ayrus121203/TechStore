using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
namespace TechStore
{
    public partial class EnableDeviceProxAuth : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserID"] != null)
            {
                GetDeviceNameAndAddress();
            }
            else
            {
                Response.Redirect("Login");
            }
        }
        private void GetDeviceNameAndAddress()
        {
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
                        System.Diagnostics.Debug.WriteLine(pairedDevice.DeviceName);
                        System.Diagnostics.Debug.WriteLine(pairedDevice.DeviceAddress);
                        System.Diagnostics.Debug.WriteLine("Device connected: " + pairedDevice.Connected);
                        System.Diagnostics.Debug.WriteLine("-----------------------------------------");

                        SqlConnection con = new SqlConnection(CS);
                        SqlCommand cmd = new SqlCommand("SELECT DeviceAddress FROM BluetoothDeviceAddress WHERE DeviceAddress=@DeviceAddress",con);
                        cmd.Parameters.AddWithValue("@DeviceAddress", pairedDevice.DeviceAddress.ToString());
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0) //Device Mac Address Exists
                        {
                            lblMsg.Text = "This device is already registered. Please login if this device is yours";
                        }
                        else
                        {
                            try
                            {
                                lblMsg.Text = "";
                                System.Diagnostics.Debug.WriteLine(device.DeviceName);
                                System.Diagnostics.Debug.WriteLine(device.DeviceAddress.ToString());
                                System.Diagnostics.Debug.WriteLine("Here");
                                txtDeviceName.Text = device.DeviceName.ToString();
                                txtDeviceAddress.Attributes["value"] = device.DeviceAddress.ToString();
                                client.Connect(device.DeviceAddress, BluetoothService.SerialPort);

                                byte[] message = System.Text.Encoding.ASCII.GetBytes("Hello Bluetooth");
                                client.GetStream().Write(message, 0, message.Length);
                                break;
                            }
                            catch
                            {
                                lblMsg.Text = "";
                            }
                        }
                    }
                    else
                    {
                        txtDeviceName.Text = "";
                        txtDeviceAddress.Attributes["value"] = "";
                        lblMsg.Text = "Ensure Paired Device Is Connected";
                        System.Diagnostics.Debug.WriteLine("Paired Device Not Connected");
                    }
                }
            }
            else
            {
                txtDeviceName.Text = "";
                txtDeviceAddress.Attributes["value"] = "";
                lblMsg.Text = "No Paired Devices Found";
                System.Diagnostics.Debug.WriteLine("No phone paired");
            }
            client.Close();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(txtDeviceName.Text);
            System.Diagnostics.Debug.WriteLine(txtDeviceAddress.Attributes["value"]);

            if (txtDeviceName.Text!="" && txtDeviceAddress.Attributes["value"] != "")
            {
                Int32 UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("INSERT INTO BluetoothDeviceAddress VALUES(@DeviceAddress, @UserID)", con);
                cmd.Parameters.AddWithValue("@DeviceAddress", txtDeviceAddress.Attributes["value"].ToString());
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("Profile");
            }
            else
            {
                lblMsg.Text = "Unable to add unconnected devices";
            }
            
        }
    }
}
