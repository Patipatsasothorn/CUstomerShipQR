using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Globalization;
using System.Reflection.Emit;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Configuration;
namespace CUstomerShipQR
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // รับค่า token, company, และ site จาก Query String
            string token = Request.QueryString["token"];
            string company = Request.QueryString["company"];
            string site = Request.QueryString["site"];
            Company.Text = company;

            // ตรวจสอบว่ามีค่า token, company, และ site หรือไม่
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(company) || string.IsNullOrEmpty(site))
            {
                // หากไม่มีค่า token, company, หรือ site ให้ทำการแสดงข้อความผิดพลาดหรือกระทำตามที่ต้องการ
                Response.Redirect("https://webapp.bpi-concretepile.co.th:8080/#/authen");
                return;
            }

            // ลบคำว่า "Bearer " ออกจาก Token
            var tokenString = token.Replace("Bearer ", "");
            var jwtHandler = new JwtSecurityTokenHandler();

            // อ่านและถอดรหัส Token
            var tokenDecoded = jwtHandler.ReadJwtToken(tokenString);

            // ดึงค่าของ Claim "username" ออกมา
            string empName = tokenDecoded.Claims.FirstOrDefault(claim => claim.Type == "username")?.Value;

            //  ตรวจสอบว่ามีค่า "username" หรือไม่
            if (!string.IsNullOrEmpty(empName))
            {
                // ทำสิ่งที่คุณต้องการกับค่า empName ที่ได้
                Label4.Text = empName;

            }
            if (!IsPostBack)
            {


                // เซ็ตค่าให้ DropDownList1 ด้วยข้อมูลบริษัทที่ได้จาก ViewState
                DataTable dt = (DataTable)ViewState["Company"];
                // เพิ่มรายการแรกใน DropDownList1 เพื่อให้ผู้ใช้สามารถเลือกบริษัท
                TextBox5.Text = DateTime.Now.ToString("dd/MM/yyyy");


            }

        }
        protected  void TextBox10_TextChanged(object sender, EventArgs e)
        {
            GridView9.DataSource = null;
            GridView9.DataBind();
            //// เรียก Swal.fire เพื่อแสดง Loading เป็นเวลา 5 วินาที
            //ClientScript.RegisterStartupScript(this.GetType(), "showLoading", @"
            //    Swal.fire({
            //        title: 'กำลังโหลด...',
            //        text: 'กรุณารอสักครู่',
            //        allowOutsideClick: false,
            //        timer: 1500, // เวลาแสดง (5 วินาที = 5000 มิลลิวินาที)
            //        didOpen: () => {
            //            Swal.showLoading();
            //        },
            //        willClose: () => {
            //            console.log('Loading ปิดแล้ว');
            //        }
            //    });", true);


            if (ViewState["GridData8"] != null)
            {
                ClearData();

            }
            // นำโค้ดที่ต้องการให้ทำงานเมื่อมีการเปลี่ยนแปลงใน TextBox10 มาวางที่นี่
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            string queryBinwh = @"
   SELECT 
    ud28.Company,
    ud28.Key1,
    CASE 
        WHEN ud28.Character01 = 'PACK' THEN ud28.Key2 
        ELSE ud28.ShortChar17 
    END AS Key2,
    CASE 
        WHEN ud28.Character01 = 'PACK' THEN ud28.Key3 
        ELSE ud28.ShortChar18 
    END AS Key3,
    CASE 
        WHEN ud28.Character01 = 'PACK' THEN ud28.Key4 
        ELSE ud28.ShortChar19 
    END AS Key4,
    ud28.Key5,
    ud28.ShortChar09 as Name,
    ud28.ShortChar10 as Name,
    ud28.ShortChar02,
    ud28.ShortChar04,
    ud28.Number04,
    ud28.Number09,
    ud28.Number11,
    ud28.Character09,
    ud28.ShortChar08,  
    ud28.Character01, 
CASE 
        WHEN ud28.Character01 = 'PACK' THEN ud28.Character02 
        ELSE ud28.ShortChar17 
    END AS Character02,
	ud28.Character05,
    ud28.Character06,
    ud28.Date01,
    ud28.ShortChar01,
    ud28.ShortChar03,
    ud28.ShortChar05,
    ud28.ShortChar07,
    ud100.Character01 as UD100_Character01,
    ud28.Character04,
    ud28.ShortChar20,
    shipvia.Description,
    project.WarehouseCode,
    project.BinNum,
    ud28.Character03,
	ud28.Character04,
	COUNT(ud15.Key1) AS TotalCount -- เพิ่มการนับจำนวนที่แยกตามกลุ่ม

FROM 
    Ice.UD28 ud28
LEFT JOIN Ice.UD100 ud100
ON ud28.Key5 = ud100.Key1  and ud100.Company = ud28.Company
LEFT JOIN BPI_Live.Erp.ShipVia shipvia
ON ud28.ShortChar07 = shipvia.ShipViaCode 
AND shipvia.Company =  @Company
LEFT JOIN Project project
ON project.ProjectID =  ud28.Character02 and project.Company = ud28.ShortChar06
left join UD15 ud15 on ud15.BPI_PickingList_c = ud28.Key1 and ud15.Company = ud28.Company and ud28.ShortChar08 = ud15.BPI_PartNum_c 
and ud15.ShortChar17 = ud28.ShortChar17 and ud15.ShortChar18 = ud28.ShortChar18 and ud15.ShortChar19 = ud28.ShortChar19
WHERE 
    ud28.Key1 = @Key1Value
	and ud28.Company = @Company

GROUP BY 
    ud28.Company,
    ud28.Key1,
    CASE WHEN ud28.Character01 = 'PACK' THEN ud28.Key2 ELSE ud28.ShortChar17 END,
    CASE WHEN ud28.Character01 = 'PACK' THEN ud28.Key3 ELSE ud28.ShortChar18 END,
    CASE WHEN ud28.Character01 = 'PACK' THEN ud28.Key4 ELSE ud28.ShortChar19 END,
    ud28.Key5,
    ud28.ShortChar09,
    ud28.ShortChar10,
    ud28.ShortChar02,
    ud28.ShortChar04,
    ud28.Number04,
    ud28.Number09,
    ud28.Number11,
    ud28.Character09,
    ud28.ShortChar08,
    ud28.Character01, 
    CASE WHEN ud28.Character01 = 'PACK' THEN ud28.Character02 ELSE ud28.ShortChar17 END,
    ud28.Character05,
    ud28.Character06,
    ud28.Date01,
    ud28.ShortChar01,
    ud28.ShortChar03,
    ud28.ShortChar05,
    ud28.ShortChar07,
    ud100.Character01,
    ud28.Character04,
    ud28.ShortChar20,
    shipvia.Description,
    project.WarehouseCode,
    project.BinNum,
    ud28.Character03,
    ud28.Character04
ORDER BY 
    ud28.Company
";
            string query2 = "SELECT s.Name,* FROM Ice.UD28 ud join ShipTo s on s.Company = ud.Company and s.ShipToNum = ud.ShortChar05  WHERE ud.Key1 = @Key1Value AND ud.Company = @CompanyValue  ";
            string query = "SELECT \r\n    (SELECT SUM(BPI_TranQty_c) \r\n     FROM UD15 \r\n     WHERE BPI_PickingList_c = @Key1Value  \r\n       AND Company = @CompanyValue \r\n       AND Key5 = 'Transfer') AS TotalNumber01,\r\n\r\n    (SELECT SUM(Number04) \r\n     FROM ice.ud30 \r\n     WHERE Key1 = @Key1Value \r\n       AND Company = @CompanyValue) AS TotalUD30\r\n ";
            string queryDATASAVE = "Select * From UD15 where BPI_PickingList_c = @Key1Value and Key5 = 'Transfer'";
            string queryDATASAVE27 = "Select * from ice.UD27 where Key1  = @Key1Value";



            if (!string.IsNullOrEmpty(TextBox10.Text))
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(queryDATASAVE27, connection))
                    {
                        command.Parameters.AddWithValue("@Key1Value", TextBox10.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // พบข้อมูล
                                Label9.Text = "True";
                                Label11.Text = "True";

                                Button3.Visible = false;
                                Button1.Visible = false;

                                // ปิด DataReader แรกก่อน
                                reader.Close();

                                // ทำการหาข้อมูลเพิ่มเติมหลังจากที่ Label9.Text = "true"
                                string queryDATASAVE17 = "Select * From UD15 where BPI_PickingList_c = @Key1Value and Key5 = 'Transfer' and Company = @Company";

                                using (SqlCommand command2 = new SqlCommand(queryDATASAVE17, connection))
                                {
                                    command2.Parameters.AddWithValue("@Key1Value", TextBox10.Text);
                                    command2.Parameters.AddWithValue("@Company", Company.Text);

                                    using (SqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        // เตรียม DataTable สำหรับเก็บข้อมูลจาก reader2
                                        DataTable dtGridData = new DataTable();

                                        // เพิ่มคอลัมน์ที่ต้องการใน DataTable
                                        dtGridData.Columns.Add("Valuescan");
                                        dtGridData.Columns.Add("PartNumber");
                                        dtGridData.Columns.Add("Warehouse");
                                        dtGridData.Columns.Add("Bin");
                                        dtGridData.Columns.Add("LOT");
                                        dtGridData.Columns.Add("Qty", typeof(int)); // กำหนดให้เป็น int
                                        dtGridData.Columns.Add("CheckBox02Value");
                                        dtGridData.Columns.Add("Description", typeof(string));

                                        // อ่านข้อมูลจาก reader2 และเพิ่มลงใน DataTable
                                        while (reader2.Read())
                                        {
                                            string partNumber = reader2["BPI_PartNum_c"].ToString();
                                            string lot = reader2["ShortChar10"].ToString();
                                            string warehouse = reader2["BPI_WarehouseFrom_c"].ToString();
                                            string bin = reader2["BPI_BinFrom_c"].ToString();

                                            bool found = false;

                                            // ตรวจสอบว่าข้อมูล PartNumber, Warehouse, และ Bin ซ้ำหรือไม่
                                            foreach (DataRow row in dtGridData.Rows)
                                            {
                                                if (row["PartNumber"].ToString() == partNumber &&
                                                    row["Warehouse"].ToString() == warehouse &&
                                                    row["Bin"].ToString() == bin)
                                                {
                                                    // ถ้าพบข้อมูลซ้ำ ให้เพิ่มค่า Qty
                                                    int currentQty = Convert.ToInt32(row["Qty"]);
                                                    row["Qty"] = currentQty + 1;
                                                    found = true;
                                                    break; // ออกจากลูป
                                                }
                                            }

                                            // ถ้าไม่พบข้อมูลซ้ำ ให้เพิ่มแถวใหม่
                                            if (!found)
                                            {
                                                DataRow newRow = dtGridData.NewRow();
                                                newRow["Valuescan"] = reader2["Number04"].ToString().TrimEnd('0').TrimEnd('.');
                                                newRow["PartNumber"] = partNumber;
                                                newRow["Warehouse"] = warehouse;
                                                newRow["Bin"] = bin;
                                                newRow["LOT"] = lot;
                                                newRow["Qty"] = 1; // กำหนดค่า Qty เป็น 1
                                                newRow["CheckBox02Value"] = reader2["CheckBox02"].ToString();
                                                newRow["Description"] = reader2["BPI_PartDescription_c"].ToString();

                                                dtGridData.Rows.Add(newRow);
                                            }
                                        }

                                        // แสดงผล DataTable ใน GridView10
                                        GridView10.DataSource = dtGridData;
                                        GridView10.DataBind();
                                    }
                                }
                            }
                            else
                            {

                                // เคลียร์ข้อมูลใน GridView ทั้งหมด
                                GridView10.DataSource =null;
                                GridView10.DataBind();
                                // ไม่พบข้อมูล
                                Label9.Text = "";
                                Label11.Text = "";
                                Button3.Visible = true;

                                Button1.Visible = true;
                            }
                        }
                    }
                }


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // สมมติว่า query มีการเลือกข้อมูลที่จำเป็นสำหรับ CheckBox01 และ CheckBox02

                    using (SqlCommand command = new SqlCommand(queryDATASAVE, connection))
                    {
                        command.Parameters.AddWithValue("@Key1Value", TextBox10.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // อ่านค่าจากฐานข้อมูล
                                string checkBox01Value = reader["CheckBox01"].ToString();
                                string checkBox02Value = reader["CheckBox02"].ToString();

                                // นำค่าที่อ่านได้จากฐานข้อมูลไปใส่ใน Label
                                Label9.Text = checkBox01Value;
                                Label10.Text = checkBox02Value;
                            }
                            else
                            {
                                // แสดงข้อความหากไม่พบข้อมูล
                                Label9.Text = "";
                                Label10.Text = "";
                                string script = $"error('ไม่พบข้อมูล: {TextBox10.Text}')";

                                // ลงทะเบียนสคริปต์ JavaScript
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", script, true);
                                //TextBox10.Text = "";
                                //return;
                            }
                        }
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(queryDATASAVE, connection))
                    {
                        command.Parameters.AddWithValue("@Key1Value", TextBox10.Text);
                      
                        DataTable dtGridData = null;
                        DataTable dtGridData3 = null;
                        // Clear GridView8 before updating it
                        ViewState["GridData8"] = dtGridData;

                        GridView8.DataSource = null; // เคลียร์ข้อมูลใน GridView8 ก่อน
                        GridView8.DataBind(); // เพื่อให้ข้อมูลถูกล้างออกไปก่อนที่จะใส่ข้อมูลใหม่
                        ViewState["GridData9"] = dtGridData3;
                        GridView9.DataSource = null; // เคลียร์ข้อมูลใน GridView8 ก่อน
                        GridView9.DataBind(); // เพื่อให้ข้อมูลถูกล้างออกไปก่อนที่จะใส่ข้อมูลใหม่
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                               

                                if (ViewState["GridData8"] != null)
                                {
                                    dtGridData = (DataTable)ViewState["GridData8"];

                                }
                                else
                                {
                                    dtGridData = new DataTable();
                                    dtGridData.Columns.Add("LOT", typeof(string));
                                    dtGridData.Columns.Add("Qty", typeof(int));
                                    dtGridData.Columns.Add("PartNumber", typeof(string));
                                    dtGridData.Columns.Add("Bin", typeof(string));
                                    dtGridData.Columns.Add("Warehouse", typeof(string));
                                    dtGridData.Columns.Add("Valuescan", typeof(string));
                                    dtGridData.Columns.Add("CheckBox02Value", typeof(bool));
                                    dtGridData.Columns.Add("Description", typeof(string));

                                }

                                if (ViewState["GridData9"] != null)
                                {
                                    dtGridData3 = (DataTable)ViewState["GridData9"];
                                }
                                else
                                {
                                    dtGridData3 = new DataTable();
                                    dtGridData3.Columns.Add("No", typeof(string));
                                    dtGridData3.Columns.Add("TextColumn", typeof(string));
                                    dtGridData3.Columns.Add("PartNumber", typeof(string));
                                    dtGridData3.Columns.Add("LOT", typeof(string));
                                    dtGridData3.Columns.Add("WH", typeof(string));
                                    dtGridData3.Columns.Add("BIN", typeof(string));
                                    dtGridData3.Columns.Add("WH_C", typeof(string));
                                    dtGridData3.Columns.Add("BIN_C", typeof(string));
                                    dtGridData3.Columns.Add("Character09", typeof(string));
                                    dtGridData3.Columns.Add("UOM", typeof(string));
                                    dtGridData3.Columns.Add("Character02", typeof(string));
                                    dtGridData3.Columns.Add("ProjectDescription", typeof(string));
                                    dtGridData3.Columns.Add("ShortChar16", typeof(string));
                                    dtGridData3.Columns.Add("ShortChar17", typeof(string));
                                    dtGridData3.Columns.Add("ShortChar18", typeof(string));
                                    dtGridData3.Columns.Add("ShortChar19", typeof(string));
                                    dtGridData3.Columns.Add("BPI_CreateTime_c", typeof(string));
                                    dtGridData3.Columns.Add("BPI_CreateDate_c", typeof(string));

                                }

                                while (reader.Read())
                                {
                                    TextBox5.Text = ((DateTime)reader["BPI_TranDate_c"]).ToString("dd/MM/yyyy");

                                    string partNumber = reader["BPI_PartNum_c"].ToString();
                                    string lot = reader["ShortChar10"].ToString();
                                    string warehouse = reader["BPI_WarehouseFrom_c"].ToString();
                                    string bin = reader["BPI_BinFrom_c"].ToString();

                                    bool found = false;

                                    foreach (DataRow row in dtGridData.Rows)
                                    {
                                        if (row["PartNumber"].ToString() == partNumber && row["Bin"].ToString() == bin && row["Warehouse"].ToString() == warehouse)
                                        {
                                            int currentQty = Convert.ToInt32(row["Qty"]);
                                            row["Qty"] = currentQty + 1;
                                            found = true;
                                            break;
                                        }
                                    }

                                    if (!found)
                                    {
                                        DataRow newRow = dtGridData.NewRow();
                                        newRow["Valuescan"] = reader["Number04"].ToString().TrimEnd('0').TrimEnd('.');
                                        newRow["PartNumber"] = partNumber;
                                        newRow["Warehouse"] = warehouse;
                                        newRow["Bin"] = bin;
                                        newRow["LOT"] = lot;
                                        newRow["Qty"] = 1;
                                        newRow["CheckBox02Value"] = reader["CheckBox02"].ToString();
                                        newRow["Description"] = reader["BPI_PartDescription_c"].ToString();

                                        dtGridData.Rows.Add(newRow);
                                    }

                                    // Add data to dtGridData3
                                    DataRow newRowGridData3 = dtGridData3.NewRow();
                                    newRowGridData3["No"] = reader["Key3"].ToString();
                                    newRowGridData3["TextColumn"] = reader["Key2"].ToString();
                                    newRowGridData3["PartNumber"] = reader["BPI_PartNum_c"].ToString();
                                    newRowGridData3["LOT"] = reader["ShortChar10"].ToString();
                                    newRowGridData3["WH"] = reader["BPI_WarehouseFrom_c"].ToString();
                                    newRowGridData3["BIN"] = reader["BPI_BinFrom_c"].ToString();
                                    newRowGridData3["WH_C"] = reader["BPI_WareHouseTo_c"].ToString();
                                    newRowGridData3["BIN_C"] = reader["BPI_BinTo_c"].ToString();
                                    newRowGridData3["Character09"] = reader["BPI_PartDescription_c"].ToString();
                                    newRowGridData3["UOM"] = reader["BPI_UOM_c"].ToString();
                                    newRowGridData3["Character02"] = reader["BPI_ProjectID_c"].ToString();
                                    newRowGridData3["ProjectDescription"] = reader["BPI_ProjectDesc_c"].ToString();
                                    newRowGridData3["ShortChar16"] = reader["BPI_DeliveryOrder_c"].ToString();
                                    newRowGridData3["ShortChar17"] = reader["ShortChar17"].ToString();
                                    newRowGridData3["ShortChar18"] = reader["ShortChar18"].ToString();
                                    newRowGridData3["ShortChar19"] = reader["ShortChar19"].ToString();
                                    newRowGridData3["BPI_CreateTime_c"] = reader["BPI_CreateTime_C"].ToString();
                                    newRowGridData3["BPI_CreateDate_c"] = DateTime.TryParse(reader["BPI_CreateDate_c"].ToString(), out DateTime createDate)
                                        ? createDate.ToString("yyyy-MM-dd") // จัดรูปแบบเป็นปี-เดือน-วัน
                                        : ""; // หากแปลงไม่ได้ให้เป็นค่าว่าง

                                    dtGridData3.Rows.Add(newRowGridData3);
                                }

                                // Update ViewState and GridView1
                                ViewState["GridData8"] = dtGridData;
                                GridView8.DataSource = dtGridData;
                                GridView8.DataBind();

                                // Update ViewState and GridView4
                                ViewState["GridData9"] = dtGridData3;
                                GridView9.DataSource = dtGridData3;
                                GridView9.DataBind();
                            }
                        }
                    }
                    if (ViewState["GridData8"] != null)
                    {
                        DataTable dt2 = (DataTable)ViewState["GridData8"];

                        // วนลูปผ่านแต่ละแถวใน DataTable
                        int totalQty = 0; // สร้างตัวแปรเพื่อเก็บผลรวมของ Qty
                        foreach (DataRow row2 in dt2.Rows)
                        {
                            // อ่านค่า Qty จากแถวและเพิ่มเข้าไปในผลรวม
                            totalQty += Convert.ToInt32(row2["Qty"]);
                        }

                        // กำหนดค่าผลรวมให้กับ TextBox14
                        //TextBox14.Text = totalQty.ToString();
                        TextBox14.Text = "0";
                    }

                }



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(queryBinwh, connection))
                    {
                        command.Parameters.AddWithValue("@Key1Value", TextBox10.Text);
                        command.Parameters.AddWithValue("@Company", Company.Text);

                        // เตรียม DataTable สำหรับเก็บข้อมูล
                        DataTable dataTable = new DataTable();

                        // ใช้ SqlDataAdapter เพื่อดึงข้อมูลจากฐานข้อมูลและเก็บลงใน DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        // กำหนด DataSource ของ GridView6 เป็น DataTable ที่เตรียมไว้
                        GridView6.DataSource = dataTable;

                        // แสดงข้อมูลใน GridView6
                        GridView6.DataBind();
                    }
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@Key1Value", TextBox10.Text);
                        command.Parameters.AddWithValue("@CompanyValue", Company.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                Carid.Text = reader["Key5"].ToString();
                                Compname.Text = reader["ShortChar05"].ToString();
                                TextBox2.Text = reader["Name"].ToString();
                                TextBox4.Text = Compname.Text + " : " + TextBox2.Text;
                                SupplierName.Text = reader["Key3"].ToString();
                                Release.Text = reader["Key4"].ToString();
                                Car.Text = reader["Key5"].ToString();
                                UOM.Text = reader["Character09"].ToString();
                                sent.Text = reader["Number11"].ToString();
                                starttime.Text = reader["ShortChar01"].ToString();
                                endtime.Text = reader["ShortChar02"].ToString();
                                cusid.Text = reader["ShortChar04"].ToString();
                                shipnum.Text = reader["ShortChar05"].ToString();
                                pack_so.Text = reader["ShortChar06"].ToString();
                                typecar.Text = reader["ShortChar07"].ToString();
                                Compname.Text = reader["ShortChar05"].ToString();
                                //TextBox3.Text = reader["Character09"].ToString();
                                DateTime dateValue = Convert.ToDateTime(reader["Date01"]);
                                TextBox5.Text = dateValue.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                // แสดงข้อความแจ้งเตือนหากไม่พบข้อมูล
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('ไม่พบข้อมูล')", true);
                                 TextBox10.Text = "";
                                return;
                            }
                        }
                    }
                    connection.Close();
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Key1Value", TextBox10.Text);
                        command.Parameters.AddWithValue("@CompanyValue", Company.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TextBox13.Text = reader["TotalNumber01"].ToString();
                                sumud30.Text = reader["TotalUD30"].ToString();
                                if (TextBox13.Text == null  || TextBox13.Text == "") 
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('สินค้าในใบ:\\n" + TextBox10.Text + "\\nยังไม่ได้ยิงขึ้นรถ')", true);
                                    return;
                                }
                            }
                            else
                            {
                                // แสดงข้อความแจ้งเตือนหากไม่พบข้อมูล
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('ไม่พบข้อมูล')", true);
                                
                                TextBox10.Text = "";
                                return;
                            }
                        }
                    }
                    connection.Close();

                }
            }

        }

        private void ClearData()
        {
            // เคลียร์ข้อมูลใน TextBox
            Label5.Text = "";
            Label6.Text = "";
            TextBox14.Text = "";
            TextBox13.Text = "";
            sumud30.Text = "";
            Carid.Text = "";
            Compname.Text = "";
            TextBox2.Text = "";
            //TextBox3.Text = "";
            TextBox4.Text = "";

            // เคลียร์ DataSource และทำการ DataBind ใหม่ (ถ้ามี)
            GridView6.DataSource = null;
            GridView6.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView4.DataSource = null;
            GridView4.DataBind();
            GridView5.DataSource = null;
            GridView5.DataBind();
            // เคลียร์ ViewState หรือ Session ที่ต้องการ
            ViewState["GridData"] = null;
            ViewState["GridData3"] = null;
            ViewState["GridData5"] = null;
        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //// เรียก Swal.fire เพื่อแสดง Loading เป็นเวลา 5 วินาที
            //ClientScript.RegisterStartupScript(this.GetType(), "showLoading", @"
            //    Swal.fire({
            //        title: 'กำลังโหลด...',
            //        text: 'กรุณารอสักครู่',
            //        allowOutsideClick: false,
            //        timer: 1500, // เวลาแสดง (5 วินาที = 5000 มิลลิวินาที)
            //        didOpen: () => {
            //            Swal.showLoading();
            //        },
            //        willClose: () => {
            //            console.log('Loading ปิดแล้ว');
            //        }
            //    });", true);
            TextBox1.Text = TextBox1.Text.ToUpper();

            Label7.Text = TextBox1.Text;
            GridView8.Visible = false;
            if (!string.IsNullOrEmpty(TextBox14.Text))
            {



                //if (TextBox13.Text == TextBox14.Text)
                //{
                //    // หาก TextBox13 มีค่าน้อยกว่า TextBox14
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('คุณยิงสินค้าในใบ:" + TextBox10.Text + "เกินกำหนดครับ')", true);
                //    TextBox1.Text = "";
                //    return;
                //}
            }
            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                UpdateData();

            }

        }
        private bool IsValueAlreadyExist(DataTable dt, string value)
        {

            foreach (DataRow row in dt.Rows)
            {
                if (row["TextColumn"].ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }
        protected void UpdateData()
        {


            if (ViewState["GridData3"] != null && ViewState["GridData3"] is DataTable)
            {
                DataTable dt3 = (DataTable)ViewState["GridData3"];

                // วนลูปผ่านแต่ละแถวใน DataTable
                foreach (DataRow row in dt3.Rows)
                {
                    // ตรวจสอบค่าในคอลัมน์ TextColumn ว่าตรงกับค่าที่ใส่ใน TextBox1 หรือไม่
                    if (row["TextColumn"].ToString() == TextBox1.Text)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Showerrordub", "Showerrordub()", true);
                        TextBox1.Text = "";
                        return; // หากพบค่าที่ตรงกันใน GridView4 ให้หยุดการวนลูป
                    }
                }
            }


            // ดำเนินการตรวจสอบ ViewState ของ gvOrders และอัพเดทข้อมูลใหม่ตามที่ต้องการ
            if (ViewState["GridData3"] != null)
            {
                string connectionString2 = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString2))
                {
                    connection.Open();

                    // First query to get grid data
                    string gridsave = @"SELECT  UD03.Character01,UD03.Character02, UD03.Key5, UD03.Key4, UD03.Key3, UD28.ShortChar10 AS BIN, UD28.ShortChar09 AS WH, SUM(UD28.Number04) AS Number04
                            FROM ice.ud03 AS UD03
                            JOIN Ice.UD28 AS UD28 ON UD03.Character01 = UD28.ShortChar08
                            JOIN 
                            Ice.UD15 AS UD15 ON UD03.Key1 = UD15.Key2
                            WHERE UD03.Company = @CompanyValue
                            AND UD03.Key1 = @Key1Value
                            AND UD28.Key1 = @Key1Value28
                            GROUP BY UD03.Key1, UD03.Key2, UD03.Character01, UD03.Key5, UD03.Key4, UD28.ShortChar10, UD28.ShortChar09, UD03.Key3,UD03.Character02";

                    using (SqlCommand command = new SqlCommand(gridsave, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyValue", Company.Text);
                        command.Parameters.AddWithValue("@Key1Value", TextBox1.Text);
                        command.Parameters.AddWithValue("@Key1Value28", TextBox10.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt2 = (DataTable)ViewState["GridData3"];

                            if (!IsValueAlreadyExist(dt2, TextBox1.Text))
                            {
                                while (reader.Read())
                                {
                                    DataRow newRow = dt2.NewRow();
                                    newRow["TextColumn"] = TextBox1.Text;
                                    newRow["PartNumber"] = reader["Character01"];
                                    newRow["LOT"] = reader["Key5"];
                                    newRow["WH"] = reader["WH"];
                                    newRow["BIN"] = reader["BIN"];
                                    newRow["Number04"] = reader["Number04"];
                                    newRow["PartDesc"] = reader["Character02"];


                                    dt2.Rows.Add(newRow);
                                }
                            }

                            // Update ViewState after adding grid data rows
                            ViewState["GridData3"] = dt2;
                        }
                    }

                    // Second query to get CustID
                    string custIdQuery = @"SELECT Erp.Customer.CustID
                               FROM Erp.Customer
                               INNER JOIN Erp.ShipTo ON Erp.Customer.Company = Erp.ShipTo.Company AND Erp.Customer.CustNum = Erp.ShipTo.CustNum
                               WHERE Erp.ShipTo.ShipToNum = 'W0075003' AND Erp.ShipTo.Company = 'BPI'
                               ORDER BY Erp.Customer.Company, Erp.ShipTo.ShipToNum";

                    using (SqlCommand custIdCommand = new SqlCommand(custIdQuery, connection))
                    {
                        string custId = null;

                        using (SqlDataReader custIdReader = custIdCommand.ExecuteReader())
                        {
                            if (custIdReader.Read())
                            {
                                custId = custIdReader["CustID"].ToString();
                            }
                        }

                        // Update the CustID in the same row(s) of dt2
                        DataTable dt2 = (DataTable)ViewState["GridData3"];
                        foreach (DataRow row in dt2.Rows)
                        {
                            if (row["TextColumn"].ToString() == TextBox1.Text)
                            {
                                row["CustID"] = custId;
                            }
                        }

                        // Update ViewState after adding CustID
                        ViewState["GridData3"] = dt2;
                    }
                }
            }
            else
            {
                string connectionString1 = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("TextColumn", typeof(string));
                dt2.Columns.Add("PartNumber", typeof(string));
                dt2.Columns.Add("LOT", typeof(string));
                dt2.Columns.Add("WH", typeof(string));
                dt2.Columns.Add("BIN", typeof(string));
                dt2.Columns.Add("Number04", typeof(string));
                dt2.Columns.Add("CustID", typeof(string)); // Adding CustID column
                dt2.Columns.Add("PartDesc", typeof(string)); // Adding CustID column



                using (SqlConnection connection = new SqlConnection(connectionString1))
                {
                    connection.Open();

                    // First query to get grid data
                    string gridsave = @"SELECT  UD03.Character01,UD03.Character02, UD03.Key5, UD03.Key4, UD03.Key3, UD28.ShortChar10 AS BIN, UD28.ShortChar09 AS WH, SUM(UD28.Number04) AS Number04
                            FROM ice.ud03 AS UD03
                            JOIN Ice.UD28 AS UD28 ON UD03.Character01 = UD28.ShortChar08
                            JOIN 
                            Ice.UD15 AS UD15 ON UD03.Key1 = UD15.Key2
                            WHERE UD03.Company = @CompanyValue
                            AND UD03.Key1 = @Key1Value
                            AND UD28.Key1 = @Key1Value28
                            GROUP BY UD03.Key1, UD03.Key2, UD03.Character01, UD03.Key5, UD03.Key4, UD28.ShortChar10, UD28.ShortChar09, UD03.Key3,UD03.Character02";

                    using (SqlCommand command = new SqlCommand(gridsave, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyValue", Company.Text);
                        command.Parameters.AddWithValue("@Key1Value", TextBox1.Text);
                        command.Parameters.AddWithValue("@Key1Value28", TextBox10.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataRow newRow = dt2.NewRow();
                                newRow["TextColumn"] = TextBox1.Text;
                                newRow["PartNumber"] = reader["Character01"];
                                newRow["LOT"] = reader["Key5"];
                                newRow["WH"] = reader["WH"];
                                newRow["BIN"] = reader["BIN"];
                                newRow["Number04"] = reader["Number04"];
                                newRow["PartDesc"] = reader["Character02"];


                                dt2.Rows.Add(newRow);
                            }
                        }
                    }

                    // Second query to get CustID
                    string custIdQuery = @"SELECT Erp.Customer.CustID
                               FROM Erp.Customer
                               INNER JOIN Erp.ShipTo ON Erp.Customer.Company = Erp.ShipTo.Company AND Erp.Customer.CustNum = Erp.ShipTo.CustNum
                               WHERE Erp.ShipTo.ShipToNum = 'W0075003' AND Erp.ShipTo.Company = 'BPI'
                               ORDER BY Erp.Customer.Company, Erp.ShipTo.ShipToNum";

                    using (SqlCommand custIdCommand = new SqlCommand(custIdQuery, connection))
                    {
                        string custId = null;

                        using (SqlDataReader custIdReader = custIdCommand.ExecuteReader())
                        {
                            if (custIdReader.Read())
                            {
                                custId = custIdReader["CustID"].ToString();
                            }
                        }

                        // Update the CustID in the same row(s) of dt2
                        foreach (DataRow row in dt2.Rows)
                        {
                            row["CustID"] = custId;
                        }
                    }

                    ViewState["GridData3"] = dt2;
                }
            }


            if (ViewState["GridData3"] != null && ViewState["GridData3"] is DataTable)
            {
                DataTable dt3 = (DataTable)ViewState["GridData3"];

                // เช็คว่าคอลัมน์ TextColumn มีค่าซ้ำกันหรือไม่
                bool hasDuplicate = false;
                HashSet<string> uniqueValues = new HashSet<string>(); // เก็บค่าที่ไม่ซ้ำกัน
                foreach (DataRow row in dt3.Rows)
                {
                    string textValue = row["TextColumn"].ToString();

                    if (uniqueValues.Contains(textValue))
                    {
                        hasDuplicate = true;
                        break; // หยุดการวนลูปหากพบค่าที่ซ้ำกัน
                    }
                    else
                    {
                        uniqueValues.Add(textValue); // เพิ่มค่าลงใน HashSet เพื่อใช้ในการเช็คซ้ำ
                    }
                }

                // หากพบค่าที่ซ้ำกันในคอลัมน์ TextColumn
                if (hasDuplicate)
                {
                    // แสดงข้อความแจ้งเตือนผ่าน JavaScript
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('สแกนซ้ำ 936')", true);
                    DataTable dt2 = (DataTable)ViewState["GridData"];

                    int totalQty = 0; // สร้างตัวแปรเพื่อเก็บผลรวมของ Qty
                    if (dt2 != null)
                    {
                        foreach (DataRow row2 in dt2.Rows)
                        {
                            // อ่านค่า Qty จากแถวและเพิ่มเข้าไปในผลรวม
                            totalQty += Convert.ToInt32(row2["Qty"]);
                        }
                    }

                    DataTable dt4 = ViewState["GridData3"] as DataTable;

                    // Compare data in GridView2 with GridView4 and delete matching rows
                    if (dt4 != null)
                    {

                        string key1Value = TextBox1.Text; // Assuming "Key1" is the column name

                        for (int i = dt4.Rows.Count - 1; i >= 0; i--)
                        {
                            string key4Value = dt4.Rows[i]["TextColumn"].ToString(); // Assuming "Key4" is the column name

                            if (key1Value == key4Value)
                            {
                                dt4.Rows.RemoveAt(i);
                            }

                        }

                        // Rebind GridView4 after removing matching rows
                        GridView4.DataSource = dt4;
                        GridView4.DataBind();
                        TextBox1.Text = "";
                    }
                    TextBox14.Text = totalQty.ToString();

                    return;
                }
            }
            // หากคุณใช้ GridView ที่เป็น TemplateField เช่น gvOrders
            // คุณต้องอัพเดท DataSource และ Bind ข้อมูลใหม่ให้กับ gvOrders ด้วย
            GridView4.DataSource = ViewState["GridData3"];
            GridView4.DataBind();


            // ดึงข้อมูลที่ใส่ใน TextBox1
            string inputKey2 = TextBox1.Text.Trim();




            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            string query = @"SELECT DISTINCT 
                    UD03.Key2,
                    UD03.Character01, 
                    UD03.Key5,
                    UD03.Key4,
                    UD03.Key1,
                    UD28.Character09,
					UD15.BPI_BinFrom_c,
					UD15.BPI_WarehouseFrom_c,
                    UD03.Key3,
                    SUM(UD28.Number04) AS Number04 
                FROM ice.ud03 AS UD03 
                JOIN Ice.UD28 AS UD28 
                    ON UD03.Character01 = UD28.ShortChar08 
                JOIN UD15 AS UD15 
                    ON UD03.Key1 = UD15.Key2  and ud15.BPI_PickingList_c = UD28.Key1 and UD28.Company = ud15.Company
            WHERE UD03.Company = @CompanyValue  
                    AND UD03.Key1 = @Key1value   
                    AND UD28.Key1 = @Key1value28 
                    AND UD15.Key5 = 'Transfer' 
                GROUP BY UD03.Key2, 
                         UD03.Key5,  
                         UD03.Character01,  
                         UD03.Key4,   
                         UD03.Key1,
                         UD28.ShortChar10,
                         UD28.ShortChar09,
                         UD03.Key3,
                         UD28.Character09,
						 UD15.BPI_BinFrom_c,
						 UD15.BPI_WarehouseFrom_c";
            string query3 = "SELECT * FROM Ice.UD28 WHERE Key1 = @Key1Value AND Company = @CompanyValue ";
            string queryPart = "SELECT Key2 FROM ice.ud03 WHERE  Company = @CompanyValue AND Key1 = @Key1value";
            string queryvalue = "SELECT Key2,Key3,ShortChar08,ShortChar10 AS Bin ,ShortChar09 AS WH,  SUM(Number04) AS Number04 FROM     Ice.UD28 WHERE   Key1 =  @Key1Value GROUP BY   Key2,Key3,ShortChar08,ShortChar10,ShortChar09;";
            string queryvalueqty = "SELECT DISTINCT  UD03.Key3,UD03.Key4,UD03.Key2, SUM(UD28.Number04) AS Number04 FROM Ice.Ud03 UD03 JOIN Ice.UD28 UD28 ON UD03.Character01 = UD28.ShortChar08 WHERE UD03.Key1 = @Key1value AND UD28.Key1 = @Key1value28   GROUP BY UD03.Key3,UD03.Key4,UD03.Key2;";
            DataTable dt;


            DataTable dtGrid5 = new DataTable(); // กำหนดค่าเริ่มต้นให้ dtGrid5 เป็น DataTable เปล่าๆ

   

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryvalueqty, connection))
                {
                    command.Parameters.AddWithValue("@Key1Value", TextBox1.Text); // Add Key1Value from TextBox1
                    command.Parameters.AddWithValue("@Key1Value28", TextBox10.Text); // Add Key1Value28 from TextBox10


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if GridData5 exists in ViewState
                        if (ViewState["GridData5"] != null)
                        {
                            dtGrid5 = (DataTable)ViewState["GridData5"];
                        }
                        else
                        {
                            // If GridData5 doesn't exist, create a new DataTable with columns
                            dtGrid5.Columns.Add("Key2", typeof(string));
                            dtGrid5.Columns.Add("Bin", typeof(string));   
                            dtGrid5.Columns.Add("WH", typeof(string));
                            dtGrid5.Columns.Add("ColumnQty", typeof(int));
                            dtGrid5.Columns.Add("Number04", typeof(double)); // Add Number04 column
                        }


                        // Save the DataTable to ViewState and bind it to GridView5
                        ViewState["GridData5"] = dtGrid5;
                        GridView5.DataSource = dtGrid5;
                        GridView5.DataBind();
                    }
                }
                connection.Close(); // Close the database connection

            }

            DataTable dtQueryValue = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand commandQueryValue = new SqlCommand(queryvalue, connection))
                {
                    commandQueryValue.Parameters.AddWithValue("@Key1Value", TextBox10.Text); // รับค่า TextBox10 แทนที่ @Key1Value
                    using (SqlDataAdapter adapter = new SqlDataAdapter(commandQueryValue))
                    {
                        adapter.Fill(dtQueryValue); // เติมข้อมูลจาก queryvalue ลงใน DataTable dtQueryValue
                        dtQueryValue.Columns.Add("QTY", typeof(int)); // เพิ่มคอลัมน์ QTY ใน DataTable ถ้ายังไม่มี

                        // กำหนดค่าเริ่มต้น QTY เป็น 0 ทุกแถวใน dtQueryValue
                        foreach (DataRow row in dtQueryValue.Rows)
                        {
                            row["QTY"] = 0;
                        }

                        // ตรวจสอบความตรงกันของข้อมูลระหว่าง dtQueryValue กับ ViewState["GridData3"]
                        DataTable dtGridData3 = ViewState["GridData3"] as DataTable;

                        foreach (DataRow row in dtGridData3.Rows)
                        {
                            string partNumber = row["PartNumber"].ToString();
                            string wh = row["WH"].ToString();
                            string bin = row["BIN"].ToString();

                            // Check if there's a match in dtQueryValue based on the conditions
                            DataRow[] matchingRows = dtQueryValue.Select($"ShortChar08 = '{partNumber}' AND WH = '{wh}' AND BIN = '{bin}'");

                            if (matchingRows.Length > 0)
                            {
                                // Loop through all matching rows (in case there are multiple matches)
                                for (int i = 0; i < matchingRows.Length; i++)
                                {
                                    DataRow matchingRow = matchingRows[i];

                                    // Increment the QTY value
                                    int currentQty = Convert.ToInt32(matchingRow["QTY"]);
                                    int number04Value = Convert.ToInt32(matchingRow["Number04"]);

                                    if (currentQty < number04Value)
                                    {
                                        matchingRow["QTY"] = currentQty + 1;

                                        // Output Key2 and Key3 as needed
                                        Label5.Text = matchingRow["Key2"].ToString();
                                        Label6.Text = matchingRow["Key3"].ToString();

                                        break; // Exit the loop if we increment QTY successfully
                                    }
                                    else if(currentQty >= number04Value)
                                    {
                                        // If QTY exceeds Number04, proceed to the next matching row (if exists)
                                        if (i + 1 < matchingRows.Length)
                                        {
                                            DataRow nextMatchingRow = matchingRows[i + 1];
                                            nextMatchingRow["QTY"] = Convert.ToInt32(nextMatchingRow["QTY"]) + 1;

                                            // Output Key2 and Key3 as needed for the next matching row
                                            Label5.Text = nextMatchingRow["Key2"].ToString();
                                            Label6.Text = nextMatchingRow["Key3"].ToString();

                                            break; // Exit the loop
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // No match found, handle this case if needed
                            }
                        }


                        // กำหนด DataSource ของ GridView3 เป็น DataTable ที่ได้จาก queryvalue
                        GridView3.DataSource = dtQueryValue;
                        GridView3.DataBind();
                    }
                }
            }


            // หากคุณใช้ ViewState สำหรับเก็บข้อมูล GridView4 แล้ว
            if (ViewState["GridData7"] != null)
            {
                string connectionString2 = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString2))
                {
                    connection.Open();
                    // First query to get grid data
                    string gridsave = @"SELECT  UD03.Character01,UD03.Character02, UD03.Key5, UD03.Key4, UD03.Key3, UD28.Character04 AS BIN, UD28.Character03 AS WH, SUM(UD28.Number04) AS Number04,UD15.ShortChar17,UD15.ShortChar18,UD15.ShortChar19
                    FROM ice.ud03 AS UD03
                    JOIN Ice.UD28 AS UD28 ON UD03.Character01 = UD28.ShortChar08
                    JOIN 
                    UD15 AS UD15 ON UD03.Key1 = UD15.Key2 and ud15.BPI_PickingList_c = ud28.Key1 and ud15.Company = ud28.Company
                    WHERE UD03.Company = @CompanyValue
                    AND UD03.Key1 = @Key1Value
                    AND UD28.Key1 = @Key1Value28
                    AND UD15.Key5 = 'Transfer'
                    GROUP BY UD03.Key1, UD03.Key2, UD03.Character01, UD03.Key5, UD03.Key4, UD28.Character04, UD28.Character03, UD03.Key3,UD03.Character02,UD15.ShortChar17,UD15.ShortChar18,UD15.ShortChar19";

                    using (SqlCommand command = new SqlCommand(gridsave, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyValue", Company.Text);
                        command.Parameters.AddWithValue("@Key1Value", TextBox1.Text);
                        command.Parameters.AddWithValue("@Key1Value28", TextBox10.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt7 = (DataTable)ViewState["GridData7"];

                            if (!IsValueAlreadyExist(dt7, TextBox1.Text))
                            {
                                while (reader.Read())
                                {
                                    DataRow newRow = dt7.NewRow();
                                    newRow["TextColumn"] = TextBox1.Text;
                                    newRow["PartNumber"] = reader["Character01"];
                                    newRow["LOT"] = reader["Key5"];
                                    newRow["WH"] = reader["WH"];
                                    newRow["BIN"] = reader["BIN"];
                                    newRow["Number04"] = reader["Number04"];
                                    newRow["Key2"] = Label5.Text;
                                    newRow["Line"] = Label6.Text;
                                    newRow["PartDesc"] = reader["Character02"];
                                    newRow["ShortChar17"] = reader["ShortChar17"];
                                    newRow["ShortChar18"] = reader["ShortChar18"];
                                    newRow["ShortChar19"] = reader["ShortChar19"];
                                    dt7.Rows.Add(newRow);
                                }
                            }

                            // Update ViewState after adding grid data rows
                            ViewState["GridData7"] = dt7;
                        }
                    }

                    // Second query to get CustID
                    string custIdQuery = @"SELECT Erp.Customer.CustID
                        FROM Erp.Customer
                        INNER JOIN Erp.ShipTo ON Erp.Customer.Company = Erp.ShipTo.Company AND Erp.Customer.CustNum = Erp.ShipTo.CustNum
                        WHERE Erp.ShipTo.ShipToNum = 'W0075003' AND Erp.ShipTo.Company = 'BPI'
                        ORDER BY Erp.Customer.Company, Erp.ShipTo.ShipToNum";

                    using (SqlCommand custIdCommand = new SqlCommand(custIdQuery, connection))
                    {
                        string custId = null;

                        using (SqlDataReader custIdReader = custIdCommand.ExecuteReader())
                        {
                            if (custIdReader.Read())
                            {
                                custId = custIdReader["CustID"].ToString();
                            }
                        }

                        // Update the CustID in the same row(s) of dt7
                        DataTable dt7 = (DataTable)ViewState["GridData7"];
                        foreach (DataRow row in dt7.Rows)
                        {
                            if (row["TextColumn"].ToString() == TextBox1.Text)
                            {
                                row["CustID"] = custId;
                            }
                        }

                        // Update ViewState after adding CustID
                        ViewState["GridData7"] = dt7;
                    }

                }
            }
            else
            {
                string connectionString1 = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                DataTable dt10 = new DataTable();
                dt10.Columns.Add("TextColumn", typeof(string));
                dt10.Columns.Add("PartNumber", typeof(string));
                dt10.Columns.Add("LOT", typeof(string));
                dt10.Columns.Add("WH", typeof(string));
                dt10.Columns.Add("BIN", typeof(string));
                dt10.Columns.Add("Number04", typeof(string));
                dt10.Columns.Add("CustID", typeof(string)); // Adding CustID column
                dt10.Columns.Add("Key2", typeof(string));
                dt10.Columns.Add("Line", typeof(string)); // Adding CustID column
                dt10.Columns.Add("PartDesc", typeof(string)); // Adding CustID column
                dt10.Columns.Add("ShortChar17", typeof(string)); // Adding CustID column
                dt10.Columns.Add("ShortChar18", typeof(string)); // Adding CustID column
                dt10.Columns.Add("ShortChar19", typeof(string)); // Adding CustID column


                using (SqlConnection connection = new SqlConnection(connectionString1))
                {
                    connection.Open();

                    // First query to get grid data
                    string gridsave = @"SELECT  UD03.Character01,UD03.Character02, UD03.Key5, UD03.Key4, UD03.Key3, UD28.Character04 AS BIN, UD28.Character03 AS WH, SUM(UD28.Number04) AS Number04,UD15.ShortChar17,UD15.ShortChar18,UD15.ShortChar19
                 FROM ice.ud03 AS UD03
                 JOIN Ice.UD28 AS UD28 ON UD03.Character01 = UD28.ShortChar08
                 JOIN 
                 UD15 AS UD15 ON UD03.Key1 = UD15.Key2 and ud15.BPI_PickingList_c = ud28.Key1 and ud15.Company = ud28.Company
                 WHERE UD03.Company = @CompanyValue
                 AND UD03.Key1 = @Key1Value
                 AND UD28.Key1 = @Key1Value28
                 AND UD15.Key5 = 'Transfer'
                 GROUP BY UD03.Key1, UD03.Key2, UD03.Character01, UD03.Key5, UD03.Key4, UD28.Character04, UD28.Character03, UD03.Key3,UD03.Character02,UD15.ShortChar17,UD15.ShortChar18,UD15.ShortChar19";

                    using (SqlCommand command = new SqlCommand(gridsave, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyValue", Company.Text);
                        command.Parameters.AddWithValue("@Key1Value", TextBox1.Text);
                        command.Parameters.AddWithValue("@Key1Value28", TextBox10.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataRow newRow = dt10.NewRow();
                                newRow["TextColumn"] = TextBox1.Text;
                                newRow["PartNumber"] = reader["Character01"];
                                newRow["LOT"] = reader["Key5"];
                                newRow["WH"] = reader["WH"];
                                newRow["BIN"] = reader["BIN"];
                                newRow["Number04"] = reader["Number04"];
                                newRow["Key2"] = Label5.Text;
                                newRow["Line"] = Label6.Text;
                                newRow["PartDesc"] = reader["Character02"];
                                newRow["ShortChar17"] = reader["ShortChar17"];
                                newRow["ShortChar18"] = reader["ShortChar18"];
                                newRow["ShortChar19"] = reader["ShortChar19"];

                                dt10.Rows.Add(newRow);
                            }
                        }
                    }

                    // Second query to get CustID
                    string custIdQuery = @"SELECT Erp.Customer.CustID
                    FROM Erp.Customer
                    INNER JOIN Erp.ShipTo ON Erp.Customer.Company = Erp.ShipTo.Company AND Erp.Customer.CustNum = Erp.ShipTo.CustNum
                    WHERE Erp.ShipTo.ShipToNum = 'W0075003' AND Erp.ShipTo.Company = 'BPI'
                    ORDER BY Erp.Customer.Company, Erp.ShipTo.ShipToNum";

                    using (SqlCommand custIdCommand = new SqlCommand(custIdQuery, connection))
                    {
                        string custId = null;

                        using (SqlDataReader custIdReader = custIdCommand.ExecuteReader())
                        {
                            if (custIdReader.Read())
                            {
                                custId = custIdReader["CustID"].ToString();
                            }
                        }

                        // Update the CustID in the same row(s) of dt2
                        foreach (DataRow row in dt10.Rows)
                        {
                            row["CustID"] = custId;
                        }
                    }

                    ViewState["GridData7"] = dt10;
                }
            }


            if (ViewState["GridData7"] != null && ViewState["GridData7"] is DataTable)
            {
                DataTable dt3 = (DataTable)ViewState["GridData7"];

                // เช็คว่าคอลัมน์ TextColumn มีค่าซ้ำกันหรือไม่
                bool hasDuplicate = false;
                HashSet<string> uniqueValues = new HashSet<string>(); // เก็บค่าที่ไม่ซ้ำกัน
                foreach (DataRow row in dt3.Rows)
                {
                    string textValue = row["TextColumn"].ToString();

                    if (uniqueValues.Contains(textValue))
                    {
                        hasDuplicate = true;
                        break; // หยุดการวนลูปหากพบค่าที่ซ้ำกัน
                    }
                    else
                    {
                        uniqueValues.Add(textValue); // เพิ่มค่าลงใน HashSet เพื่อใช้ในการเช็คซ้ำ
                    }
                }

                // หากพบค่าที่ซ้ำกันในคอลัมน์ TextColumn
                if (hasDuplicate)
                {
                    // แสดงข้อความแจ้งเตือนผ่าน JavaScript
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('สแกนซ้ำ 1365')", true);
                    DataTable dt2 = (DataTable)ViewState["GridData"];

                    int totalQty = 0; // สร้างตัวแปรเพื่อเก็บผลรวมของ Qty
                    if (dt2 != null)
                    {
                        foreach (DataRow row2 in dt2.Rows)
                        {
                            // อ่านค่า Qty จากแถวและเพิ่มเข้าไปในผลรวม
                            totalQty += Convert.ToInt32(row2["Qty"]);
                        }
                    }

                    DataTable dt4 = ViewState["GridData7"] as DataTable;

                    // Compare data in GridView2 with GridView4 and delete matching rows
                    if (dt4 != null)
                    {

                        string key1Value = TextBox1.Text; // Assuming "Key1" is the column name

                        for (int i = dt4.Rows.Count - 1; i >= 0; i--)
                        {
                            string key4Value = dt4.Rows[i]["TextColumn"].ToString(); // Assuming "Key4" is the column name

                            if (key1Value == key4Value)
                            {
                                dt4.Rows.RemoveAt(i);
                            }

                        }

                        // Rebind GridView4 after removing matching rows
                        GridView7.DataSource = dt4;
                        GridView7.DataBind();
                        TextBox1.Text = "";
                    }
                    TextBox14.Text = totalQty.ToString();

                    return;
                }
            }
            // หากคุณใช้ GridView ที่เป็น TemplateField เช่น gvOrders
            // คุณต้องอัพเดท DataSource และ Bind ข้อมูลใหม่ให้กับ gvOrders ด้วย
            GridView7.DataSource = ViewState["GridData7"];
            GridView7.DataBind();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command2 = new SqlCommand(query3, connection))
                {
                    command2.Parameters.AddWithValue("@Key1Value", TextBox10.Text);
                    command2.Parameters.AddWithValue("@CompanyValue", Company.Text);

                    using (SqlDataReader reader = command2.ExecuteReader())
                    {
                        if (reader.Read())
                        {



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Showerror", "Showerror()", true);
                            GridView3.DataSource = null;
                        }
                    }
                }




                // เชื่อมต่อกับฐานข้อมูลและดึงข้อมูล
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Key1value28", TextBox10.Text);
                    command.Parameters.AddWithValue("@CompanyValue", Company.Text);
                    command.Parameters.AddWithValue("@Key1value", TextBox1.Text);


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                if (ViewState["GridData"] != null)
                                {
                                    dt = (DataTable)ViewState["GridData"];


                                }

                                else
                                {
                                    dt = new DataTable();
                                    dt.Columns.Add("LOT", typeof(string));
                                    dt.Columns.Add("Qty", typeof(int));
                                    dt.Columns.Add("PartNumber", typeof(string));
                                    dt.Columns.Add("Bin", typeof(string));
                                    dt.Columns.Add("Warehouse", typeof(string));
                                    dt.Columns.Add("Valuescan", typeof(string));
                                    dt.Columns.Add("Description", typeof(string));

                                }

                                bool found = false;
                                string partNumber = reader["Character01"].ToString();
                                string lot = reader["Key5"].ToString();
                                string warehouse = reader["BPI_WarehouseFrom_c"].ToString();
                                string bin = reader["BPI_BinFrom_c"].ToString();


                                foreach (DataRow row in dt.Rows)
                                {
                                    if (row["PartNumber"].ToString() == partNumber && row["Bin"].ToString() == bin && row["Warehouse"].ToString() == warehouse)
                                    {
                                        int currentQty = Convert.ToInt32(row["Qty"]);
                                        row["Qty"] = currentQty + 1;
                                        found = true;
                                        break;
                                    }
                                }


                                if (!found)
                                {


                                    DataRow newRow = dt.NewRow();


                                    newRow["Valuescan"] = reader["Number04"].ToString().TrimEnd('0').TrimEnd('.'); ;
                                    newRow["PartNumber"] = reader["Character01"].ToString();
                                    newRow["Warehouse"] = reader["BPI_WarehouseFrom_c"].ToString();
                                    newRow["Bin"] = reader["BPI_BinFrom_c"].ToString();
                                    newRow["LOT"] = reader["Key5"].ToString();
                                    newRow["Qty"] = 1;
                                    newRow["Description"] = reader["Character09"].ToString();

                                    dt.Rows.Add(newRow);

                                }

                                ViewState["GridData"] = dt;
                                GridView1.DataSource = dt;
                                GridView1.DataBind();


                            }
                            DataTable dt2 = (DataTable)ViewState["GridData"];

                            // วนลูปผ่านแต่ละแถวใน DataTable
                            int totalQty = 0; // สร้างตัวแปรเพื่อเก็บผลรวมของ Qty
                            foreach (DataRow row2 in dt2.Rows)
                            {
                                // อ่านค่า Qty จากแถวและเพิ่มเข้าไปในผลรวม
                                totalQty += Convert.ToInt32(row2["Qty"]);
                            }

                            // กำหนดค่าผลรวมให้กับ TextBox14
                            TextBox14.Text = totalQty.ToString();
                        }
                        else
                        {
                           
                            //หากไม่พบข้อมูล ให้แสดงข้อความแจ้งเตือน
                            string script = $"error('Serial \\n{TextBox1.Text}นี้ไม่พบข้อมูลในใบเบิก:\\n {TextBox10.Text}')";

                            // ลงทะเบียนสคริปต์ JavaScript
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", script, true);
                            DataTable dt2 = (DataTable)ViewState["GridData"];

                            int totalQty = 0; // สร้างตัวแปรเพื่อเก็บผลรวมของ Qty
                            if (dt2 != null)
                            {
                                foreach (DataRow row2 in dt2.Rows)
                                {
                                    // อ่านค่า Qty จากแถวและเพิ่มเข้าไปในผลรวม
                                    totalQty += Convert.ToInt32(row2["Qty"]);
                                }
                            }
                            DataTable dt4 = ViewState["GridData3"] as DataTable;
                            DataTable dt5 = ViewState["GridData3"] as DataTable;

                            // Compare data in GridView2 with GridView4 and delete matching rows
                            if (dt4 != null)
                            {

                                string key1Value = TextBox1.Text; // Assuming "Key1" is the column name

                                for (int i = dt4.Rows.Count - 1; i >= 0; i--)
                                {
                                    string key4Value = dt4.Rows[i]["TextColumn"].ToString(); // Assuming "Key4" is the column name

                                    if (key1Value == key4Value)
                                    {
                                        dt4.Rows.RemoveAt(i);
                                    }

                                }

                                // Rebind GridView4 after removing matching rows
                                GridView4.DataSource = dt4;

                                //    GridView4.DataBind();
                                TextBox1.Text = "";
                            }
                            // Compare data in GridView2 with GridView4 and delete matching rows
                            if (dt5 != null)
                            {

                                string key1Value = TextBox1.Text; // Assuming "Key1" is the column name

                                for (int i = dt5.Rows.Count - 1; i >= 0; i--)
                                {
                                    string key4Value = dt5.Rows[i]["TextColumn"].ToString(); // Assuming "Key4" is the column name

                                    if (key1Value == key4Value)
                                    {
                                        dt5.Rows.RemoveAt(i);
                                    }

                                }

                                // Rebind GridView4 after removing matching rows
                                GridView7.DataSource = dt5;
                            }
                            TextBox14.Text = totalQty.ToString();
                            return;
                        }
                    }
                    connection.Close(); // ปิดการเชื่อมต่อกับฐานข้อมูล
                }


            }

            TextBox1.Text = "";

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        private string GetCellValue(GridViewRow row, int cellIndex)
        {
            if (cellIndex < 0 || cellIndex >= row.Cells.Count)
            {
                return ""; // or handle this case as needed
            }

            var cell = row.Cells[cellIndex];
            string value = cell.Text;
            if (string.IsNullOrEmpty(value) || value == "&nbsp;")
            {
                return "";
            }
            return value;
        }


        protected async void Button3_Click1(object sender, EventArgs e)
        {
            int Value = Convert.ToInt32(TextBox14.Text);
            int Wantsent = Convert.ToInt32(TextBox13.Text);
          
            if (Value < Wantsent)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('จำนวนน้อยกว่า ต้องการส่ง')", true);
                return;

            }
            if (Value > Wantsent)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('จำนวนมากกว่า ต้องการส่ง')", true);
                return;

            }
            DateTime date = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string formattedDate = date.ToString("yyyy-MM-dd");
            DateTime now = DateTime.Now;
            string formattedNow = now.ToString("yyyy-MM-dd");

            await Task.Run(async () =>
            {
                try
                {
                    // สร้าง HttpClient และเตรียม token
                    using (HttpClient client = new HttpClient())
                    {
                        //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOiIxNzE4MDIzMDU4IiwiaWF0IjoiMTcxODAxMTY1OCIsImlzcyI6ImVwaWNvciIsImF1ZCI6ImVwaWNvciIsInVzZXJuYW1lIjoiUGF0aXBhdC5zIn0._0nlVa6bPRqyYZmMvGsbgzKZCQt7TPb1W_LOI4p-T4E";
                        string token = Request.QueryString["token"];
                        string site = Request.QueryString["site"];

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string apiUrlUD17 = "https://erp.bpi-concretepile.co.th/BPI_Live/api/v1/Ice.BO.UD17Svc/UD17s/";
                        string apiUrlUD27 = "https://erp.bpi-concretepile.co.th/BPI_Live/api/v1/Ice.BO.UD27Svc/UD27s/";
                        HashSet<string> key5Set = new HashSet<string>();
                            // จาก GridView6
                            foreach (GridViewRow row6 in GridView6.Rows)
                            {
                                string connectionString1 = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                                string checkQueryUD27 = @"
                                        SELECT COUNT(*)
                                        FROM Ice.UD27
                                        WHERE Key1 = @Key1 AND Key2 = @Key2 AND Key3 = @Key3 AND Key4 = @Key4 AND Key5 = @Key5 AND Company =@Company ";

                                using (SqlConnection connection = new SqlConnection(connectionString1))
                                {
                                    await connection.OpenAsync();

                                    using (SqlCommand command = new SqlCommand(checkQueryUD27, connection))
                                    {
                                        command.Parameters.AddWithValue("@Company", Request.QueryString["company"]);
                                        command.Parameters.AddWithValue("@Key1", TextBox10.Text);
                                        command.Parameters.AddWithValue("@Key2", GetCellValue(row6, 2));
                                        command.Parameters.AddWithValue("@Key3", GetCellValue(row6, 3));
                                        command.Parameters.AddWithValue("@Key4", GetCellValue(row6, 4));
                                        command.Parameters.AddWithValue("@Key5", GetCellValue(row6, 5));

                                        int count = (int)await command.ExecuteScalarAsync();

                                        if (count > 0)
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);
                                            continue; // Skip to the next iteration
                                        }
                                    // ดึงค่าจาก GridView8 คอลัมน์ PartNumber และ Qty
                                    string partNumber = GetCellValue(row6, 14); // สมมุติว่า ShortChar08 อยู่ในคอลัมน์ที่ 14 ของ GridView6
                                    string qtyValue = ""; // ค่าที่จะนำไปใช้ใน Number10

                                            foreach (GridViewRow row8 in GridView8.Rows)
                                            {
                                               string gridView8PartNumber = row8.Cells[1].Text; // ดึง PartNumber จาก GridView8
                                                        if (partNumber == gridView8PartNumber) // เช็คเงื่อนไข PartNumber ตรงกับ ShortChar08 หรือไม่
                                                        {
                                                            qtyValue = row8.Cells[7].Text; // ดึงค่า Qty จาก GridView8 เมื่อเงื่อนไขตรงกัน
                                                            break; // ออกจากลูปเมื่อเจอค่า
                                                        }
                                                        else 
                                                        {
                                                            qtyValue = "0";
                                                        }
                                            }
                                    var requestDataUD27 = new
                                    {
                                        Company = Request.QueryString["company"],

                                            //Company = "BPI",
                                            Key1 = TextBox10.Text, //Picking List
                                            Key2 = string.IsNullOrEmpty(GetCellValue(row6, 2)) ? "" : GetCellValue(row6, 2), //Plan ID / SO
                                            Key3 = string.IsNullOrEmpty(GetCellValue(row6, 3)) ? "" : GetCellValue(row6, 3), //Line
                                            Key4 = string.IsNullOrEmpty(GetCellValue(row6, 4)) ? "" : GetCellValue(row6, 4), //Release
                                            Key5 = string.IsNullOrEmpty(GetCellValue(row6, 5)) ? "" : GetCellValue(row6, 5), // Car ID
                                            Character01 = string.IsNullOrEmpty(GetCellValue(row6, 15)) ? "" : GetCellValue(row6, 15),
                                            Character02 = string.IsNullOrEmpty(GetCellValue(row6, 16)) ? "" : GetCellValue(row6, 16),
                                            Character03 = string.IsNullOrEmpty(GetCellValue(row6, 30)) ? "" : GetCellValue(row6, 30), // อ้างอิงจาก GridView6
                                            Character04 = string.IsNullOrEmpty(GetCellValue(row6, 31)) ? "" : GetCellValue(row6, 31), // อ้างอิงจาก GridView6
                                            Character05 = string.IsNullOrEmpty(GetCellValue(row6, 17)) ? "" : GetCellValue(row6, 17),
                                            Character06 = string.IsNullOrEmpty(GetCellValue(row6, 18)) ? "" : GetCellValue(row6, 18),
                                            Character07 = Label4.Text,
                                            Character08 = Label4.Text,
                                            Character09 = string.IsNullOrEmpty(GetCellValue(row6, 13)) ? "" : GetCellValue(row6, 13),
                                            Number04 = string.IsNullOrEmpty(GetCellValue(row6, 32)) ? "" : GetCellValue(row6, 32), // อ้างอิงจาก GridView6
                                            Number09 = string.IsNullOrEmpty(GetCellValue(row6, 11)) ? "" : GetCellValue(row6, 11), // อ้างอิงจาก GridView6
                                            Number10 = TextBox14.Text, // อ้างอิงจาก GridView6
                                            Number11 = string.IsNullOrEmpty(GetCellValue(row6, 12)) ? "" : GetCellValue(row6, 12), // อ้างอิงจาก GridView6
                                            Date01 = formattedDate,
                                            Date02 = formattedNow,
                                            Date03 = DateTime.Now.Date,
                                            ShortChar01 = string.IsNullOrEmpty(GetCellValue(row6, 20)) ? "" : GetCellValue(row6, 20),
                                            ShortChar02 = string.IsNullOrEmpty(GetCellValue(row6, 8)) ? "" : GetCellValue(row6, 8),
                                            ShortChar03 = string.IsNullOrEmpty(GetCellValue(row6, 21)) ? "" : GetCellValue(row6, 21),
                                            ShortChar04 = string.IsNullOrEmpty(GetCellValue(row6, 9)) ? "" : GetCellValue(row6, 9),
                                            ShortChar05 = string.IsNullOrEmpty(GetCellValue(row6, 22)) ? "" : GetCellValue(row6, 22),
                                            ShortChar06 = string.IsNullOrEmpty(GetCellValue(row6, 15)) ? "" : GetCellValue(row6, 15),
                                            ShortChar07 = string.IsNullOrEmpty(GetCellValue(row6, 23)) ? "" : GetCellValue(row6, 23),
                                            ShortChar08 = string.IsNullOrEmpty(GetCellValue(row6, 14)) ? "" : GetCellValue(row6, 14),
                                            ShortChar10 = string.IsNullOrEmpty(GetCellValue(row6, 27)) ? "" : GetCellValue(row6, 27),
                                            ShortChar12 = string.IsNullOrEmpty(GetCellValue(row6, 24)) ? "" : GetCellValue(row6, 24),
                                            ShortChar13 = string.IsNullOrEmpty(GetCellValue(row6, 28)) ? "" : GetCellValue(row6, 28),
                                            ShortChar14 = string.IsNullOrEmpty(GetCellValue(row6, 29)) ? "" : GetCellValue(row6, 29),
                                            ShortChar19 = "Generate",
                                            ShortChar20 = site,
                                            CheckBox01 = false,
                                            CheckBox02 = false
                                        };

                                        // ส่งข้อมูลไปยัง UD27 API
                                        string jsonDataUD27 = JsonConvert.SerializeObject(requestDataUD27);
                                        HttpContent contentUD27 = new StringContent(jsonDataUD27, Encoding.UTF8, "application/json");
                                        HttpResponseMessage responseUD27 = await client.PostAsync(apiUrlUD27, contentUD27);

                                        if (responseUD27.IsSuccessStatusCode)
                                        {
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);

                                            string responseBodyUD27 = await responseUD27.Content.ReadAsStringAsync();
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "success('โอนย้าย Picking No" + TextBox10.Text + " เรียบร้อยแล้ว')", true);
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "success('บันทึกข้อมูล Picking No" + TextBox10.Text + "เรียบร้อยแล้ว')", true);

                                            Button3.Visible = false;

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('Failed to post data to UD27.')", true);
                                            Response.Write("Failed to post data to UD27. Status code: " + responseUD27.StatusCode);
                                            //return;
                                        }




                                    }

                                }
                            }
                        
                        foreach (GridViewRow row4 in GridView7.Rows)
                        {


                            // จาก GridView6
                            foreach (GridViewRow row6 in GridView6.Rows)
                            {
                                string connectionString1 = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                                string checkQueryUD27 = @"
                                        SELECT COUNT(*)
                                        FROM Ice.UD17
                                        WHERE Key1 = @Key1 AND Key2 = @Key2 AND Key3 = @Key3 AND Key4 = @Key4 AND Key5 = @Key5 AND Company =@Company ";

                                using (SqlConnection connection = new SqlConnection(connectionString1))
                                {
                                    await connection.OpenAsync();

                                    using (SqlCommand command = new SqlCommand(checkQueryUD27, connection))
                                    {
                                        command.Parameters.AddWithValue("@Company", Request.QueryString["company"]);
                                        command.Parameters.AddWithValue("@Key1", TextBox10.Text);
                                        command.Parameters.AddWithValue("@Key2", GetCellValue(row4, 7));
                                        command.Parameters.AddWithValue("@Key3", GetCellValue(row4, 8));
                                        command.Parameters.AddWithValue("@Key4", GetCellValue(row6, 4));
                                        command.Parameters.AddWithValue("@Key5", GetCellValue(row4, 0));

                                        int count = (int)await command.ExecuteScalarAsync();

                                        if (count > 0)
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);
                                            continue; // Skip to the next iteration
                                        }
                                        var requestDataUD17 = new
                                        {
                                            // ข้อมูลสำหรับ UD17
                                            Company = Request.QueryString["company"],
                                            Key1 = TextBox10.Text, //Picking List
                                            Key2 = string.IsNullOrEmpty(GetCellValue(row4, 7)) ? "" : GetCellValue(row4, 7), //Plan ID / SO
                                            Key3 = string.IsNullOrEmpty(GetCellValue(row4, 8)) ? "" : GetCellValue(row4, 8), //Line
                                            Key4 = string.IsNullOrEmpty(GetCellValue(row6, 4)) ? "" : GetCellValue(row6, 4), //Release
                                            Key5 = string.IsNullOrEmpty(GetCellValue(row4, 0)) ? "" : GetCellValue(row4, 0),
                                            ShortChar01 = "LOT-00000120221231",
                                            ShortChar08 = string.IsNullOrEmpty(GetCellValue(row4, 1)) ? "" : GetCellValue(row4, 1),
                                            ShortChar10 = string.IsNullOrEmpty(GetCellValue(row4, 2)) ? "" : GetCellValue(row4, 2),
                                            ShortChar14 = string.IsNullOrEmpty(GetCellValue(row4, 10)) ? "" : GetCellValue(row4, 10),
                                            ShortChar15 = string.IsNullOrEmpty(GetCellValue(row4, 11)) ? "" : GetCellValue(row4, 11),
                                            ShortChar16 = string.IsNullOrEmpty(GetCellValue(row4, 12)) ? "" : GetCellValue(row4, 12),
                                            Character09 = string.IsNullOrEmpty(GetCellValue(row4, 9)) ? "" : GetCellValue(row4, 9),
                                            Date01 = formattedDate,

                                            ShortChar03 = Label4.Text,
                                            Date02 = DateTime.Now.Date,
                                            ShortChar04 = DateTime.Now.ToString("HH:mm:ss"),


                                            Number03 = "1",
                                            CheckBox01 = false,
                                            CheckBox02 = false,

                                        };

                                        // ส่งข้อมูลไปยัง UD17 API
                                        string jsonDataUD17 = JsonConvert.SerializeObject(requestDataUD17);
                                        HttpContent contentUD17 = new StringContent(jsonDataUD17, Encoding.UTF8, "application/json");
                                        HttpResponseMessage responseUD17 = await client.PostAsync(apiUrlUD17, contentUD17);

                                        if (responseUD17.IsSuccessStatusCode)
                                        {
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);

                                            string responseBodyUD17 = await responseUD17.Content.ReadAsStringAsync();
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "success('โอนย้าย Picking No" + TextBox10.Text + "ลงUD17 เรียบร้อยแล้ว')", true);
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);

                                            Button3.Visible = false;
                                            Button1.Visible = false;



                                        }
                                        else
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('Failed to post data to UD17.')", true);
                                            Response.Write("Failed to post data to UD17. Status code: " + responseUD17.StatusCode);
                                        }




                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('Failed to post data to UD17.')", true);

                }
            });
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            // อัปเดตคอลัมน์ ShortChar18 ในตาราง UD17 ด้วยข้อมูล SysRowID จากตาราง UD27
            string updateQuery = @"
                          UPDATE Ice.UD17
                            SET ShortChar18 = ud27.SysRowID
                            FROM Ice.UD17 UD17
                            INNER JOIN ice.Ud27 ud27 ON UD17.Key1 =  ud27.Key1 
                            AND UD17.Company = ud27.Company 
                            AND UD17.ShortChar08 = ud27.ShortChar08
	                       AND (
                                (ud27.Character01 = 'PACK' 
		                        AND UD17.Key2 = ud27.Key2 AND UD17.Key3 = ud27.Key3 AND UD17.Key4 = ud27.Key4) 
                                OR (ud27.Character01 = 'SO'    
		                        AND UD17.ShortChar14 = ud27.Key2
		                        AND UD17.ShortChar15 = ud27.Key3
		                        AND UD17.ShortChar16 = ud27.Key4)
                            )
	                        WHere UD17.key1  = @Key1
                           ";

            // สร้างการเชื่อมต่อกับฐานข้อมูล
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // เปิดการเชื่อมต่อ
                await connection.OpenAsync();

                // สร้างคำสั่ง SQL
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Key1", TextBox10.Text);

                    // ทำการ execute คำสั่ง SQL
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // ตรวจสอบว่ามีแถวไหนถูกอัปเดตหรือไม่
                    if (rowsAffected > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);

                        // ถ้ามี แสดงว่าอัปเดตเรียบร้อย
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('อัปเดตข้อมูลใน UD17 เรียบร้อยแล้ว')", true);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('บันทึกข้อมูลเรียบร้อยแล้ว')", true);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('Failed to post data to UD17.')", true);

                    }
                    else
                    {
                        // ถ้าไม่มี แสดงว่าไม่มีข้อมูลที่ตรงกับเงื่อนไขหรืออาจเกิดข้อผิดพลาด
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('ไม่พบข้อมูลที่ตรงกับเงื่อนไขหรือเกิดข้อผิดพลาดในการอัปเดตข้อมูล')", true);
                        return;
                    }
                }
            }
        }
        private string CleanValue(string value)
        {
            if (string.IsNullOrEmpty(value) || value == "&nbsp;")
            {
                return "";
            }
            return value;
        }

        protected async void Button2_Click1(object sender, EventArgs e)
        {
            int Value = Convert.ToInt32(TextBox14.Text);
            int Wantsent = Convert.ToInt32(TextBox13.Text);
            int SUMUD30 = (int)Math.Round(Convert.ToDecimal(sumud30.Text), MidpointRounding.AwayFromZero);
            
            if (Wantsent > SUMUD30)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('จำนวนเข็มขึ้นรถ มากกว่าต้องการส่งในบิล \\n กรุณาตรวจสอบด่วน!!')", true);
                return;

            }
            DateTime date = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string formattedDate = date.ToString("yyyy-MM-dd");
            // ตรวจสอบค่า CheckBox01 และ CheckBox02 ก่อนการลบข้อมูล
            bool canDelete = true;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT CheckBox01, CheckBox02 FROM UD15 WHERE BPI_PickingList_c = @PickingList AND Key5 = 'Transfer' AND Company = @Company";
                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PickingList", TextBox10.Text);
                    cmd.Parameters.AddWithValue("@Company", Request.QueryString["company"]);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool checkBox01 = reader.GetBoolean(0);
                            bool checkBox02 = reader.GetBoolean(1);

                            // ถ้า CheckBox01 หรือ CheckBox02 เท่ากับ 1 ให้แจ้ง error และไม่ลบข้อมูล
                            if (checkBox01 || checkBox02)
                            {
                                canDelete = false;
                                break;
                            }
                        }
                    }
                    conn.Close();
                }
            }

            if (!canDelete)
            {
                // ถ้าพบว่า CheckBox01 หรือ CheckBox02 เป็น 1 ให้แจ้ง error และ return
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('ไม่สามารถโอนย้ายได้ เนื่องจากมีการโอนย้ายแล้ว')", true);
                return;
            }
            try
            {
                // วนลูป GridView9 เพื่อดึงข้อมูลที่ต้องการโอนย้าย
                foreach (GridViewRow row4 in GridView9.Rows)
                {
                    var requestData = new
                    {
                        BPI_Plant_c = Request.QueryString["site"],
                        Company = Request.QueryString["company"],
                        Key1 = CleanValue(row4.Cells[10].Text),
                        Key2 = CleanValue(row4.Cells[1].Text),
                        Key3 = CleanValue(row4.Cells[0].Text),
                        Key4 = "",
                        Key5 = "Transfer",
                        ShortChar17 = CleanValue(row4.Cells[13].Text),
                        ShortChar18 = CleanValue(row4.Cells[14].Text),
                        ShortChar19 = CleanValue(row4.Cells[15].Text),
                        BPI_ProjectID_c = CleanValue(row4.Cells[10].Text),
                        BPI_ProjectDesc_c = CleanValue(row4.Cells[11].Text),
                        BPI_PartNum_c = CleanValue(row4.Cells[2].Text),
                        BPI_PickingList_c = CleanValue(TextBox10.Text),
                        BPI_PartDescription_c = CleanValue(row4.Cells[8].Text),
                        BPI_DeliveryOrder_c = CleanValue(row4.Cells[12].Text),
                        BPI_UOM_c = CleanValue(row4.Cells[9].Text),
                        BPI_TranDate_c = formattedDate,
                        BPI_Loaded_c = "W",
                        BPI_CreateDate_c = CleanValue(row4.Cells[17].Text),
                        BPI_EmpID_c = "IT000005",
                        BPI_CreateUser_c = CleanValue(Label4.Text),
                        BPI_CreateTime_c = CleanValue(row4.Cells[16].Text),
                        BPI_BinFrom_c = CleanValue(row4.Cells[5].Text),
                        BPI_WarehouseFrom_c = CleanValue(row4.Cells[4].Text),
                        BPI_WareHouseTo_c = CleanValue(row4.Cells[6].Text),
                        BPI_BinTo_c = CleanValue(row4.Cells[7].Text),
                        BPI_LotNum_c = "LOT-00000120221231",
                        BPI_TranQty_c = "1",
                        ShortChar10 = CleanValue(row4.Cells[3].Text),
                        CheckBox02 = true
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        string token = Request.QueryString["token"];
                        string Company = Request.QueryString["Company"];

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string apiKey = ConfigurationManager.AppSettings["apikey"]; // แทนค่า YOUR_API_KEY ด้วย API Key ที่ถูกต้อง
                        client.DefaultRequestHeaders.Add("x-api-key", apiKey);

                        string apiUrl = "https://erp.bpi-concretepile.co.th/BPI_Live/api/v2/odata/" + Company+"/Ice.BO.UD15Svc/UD15s";

                        string jsonData = JsonConvert.SerializeObject(requestData);
                        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", $"successmes('โอนย้าย Picking No \\n{TextBox10.Text} \\nเรียบร้อยแล้ว')", true);
                            Label8.Text = "1";
                            Label9.Text = "True";
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                LinkButton deleteButton = (LinkButton)row.FindControl("DeleteButton");
                                deleteButton.Visible = false;

                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.Conflict)
                        {
                            continue;
                        }
                        else
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", $"error('Failed to post data to Ud15. Response: {responseBody}')", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        //protected async void Button2_Click1(object sender, EventArgs e)
        //{
        //    DateTime date = DateTime.ParseExact(TextBox5.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    string formattedDate = date.ToString("yyyy-MM-dd");

        //    try
        //    {
        //        // ตรวจสอบค่า CheckBox01 และ CheckBox02 ก่อนการลบข้อมูล
        //        bool canDelete = true;
        //        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            string checkQuery = "SELECT CheckBox01, CheckBox02 FROM UD15 WHERE BPI_PickingList_c = @PickingList AND Key5 = 'Transfer' AND Company = @Company";
        //            using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@PickingList", TextBox10.Text);
        //                cmd.Parameters.AddWithValue("@Company", Request.QueryString["company"]);

        //                conn.Open();
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        bool checkBox01 = reader.GetBoolean(0);
        //                        bool checkBox02 = reader.GetBoolean(1);

        //                        // ถ้า CheckBox01 หรือ CheckBox02 เท่ากับ 1 ให้แจ้ง error และไม่ลบข้อมูล
        //                        if (checkBox01 || checkBox02)
        //                        {
        //                            canDelete = false;
        //                            break;
        //                        }
        //                    }
        //                }
        //                conn.Close();
        //            }
        //        }

        //        if (!canDelete)
        //        {
        //            // ถ้าพบว่า CheckBox01 หรือ CheckBox02 เป็น 1 ให้แจ้ง error และ return
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('ไม่สามารถโอนย้ายได้ เนื่องจากมีการโอนย้ายแล้ว')", true);
        //            return;
        //        }

        //        // ลบข้อมูลจากตาราง UD15 ที่มี BPI_PickingList_c ตรงกับ TextBox10.Text
        //        bool deleteSuccess = false;

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            string deleteQuery = "DELETE FROM ice.UD15 WHERE SysRowID IN ( SELECT ForeignSysRowID  FROM UD15 WHERE BPI_PickingList_c = @PickingList AND Key5 = 'Transfer' AND Company = @Company );";
        //            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@PickingList", TextBox10.Text);
        //                cmd.Parameters.AddWithValue("@Company", Request.QueryString["company"]);

        //                conn.Open();
        //                int rowsAffected = cmd.ExecuteNonQuery();
        //                conn.Close();

        //                // ตรวจสอบว่ามีการลบข้อมูลหรือไม่
        //                if (rowsAffected > 0)
        //                {
        //                    deleteSuccess = true;
        //                }
        //            }
        //        }

        //        // ถ้าการลบสำเร็จ
        //        if (deleteSuccess)
        //        {
        //            // วนลูป GridView4 เพื่อดึงข้อมูลที่ต้องการโอนย้าย
        //            foreach (GridViewRow row4 in GridView9.Rows)
        //            {
        //                var requestData = new
        //                {
        //                    BPI_Plant_c = Request.QueryString["site"],
        //                    Company = Request.QueryString["company"],
        //                    Key1 = CleanValue(row4.Cells[10].Text),
        //                    Key2 = CleanValue(row4.Cells[1].Text),
        //                    Key3 = CleanValue(row4.Cells[0].Text),
        //                    Key4 = "",
        //                    Key5 = "Transfer",
        //                    ShortChar17 = CleanValue(row4.Cells[13].Text),
        //                    ShortChar18 = CleanValue(row4.Cells[14].Text),
        //                    ShortChar19 = CleanValue(row4.Cells[15].Text),
        //                    BPI_ProjectID_c = CleanValue(row4.Cells[10].Text),
        //                    BPI_ProjectDesc_c = CleanValue(row4.Cells[11].Text),
        //                    BPI_PartNum_c = CleanValue(row4.Cells[2].Text),
        //                    BPI_PickingList_c = CleanValue(TextBox10.Text),
        //                    BPI_PartDescription_c = CleanValue(row4.Cells[8].Text),
        //                    BPI_DeliveryOrder_c = CleanValue(row4.Cells[12].Text),
        //                    BPI_UOM_c = CleanValue(row4.Cells[9].Text),
        //                    BPI_TranDate_c = formattedDate,
        //                    BPI_Loaded_c = "W",
        //                    BPI_CreateDate_c = CleanValue(row4.Cells[17].Text),
        //                    BPI_EmpID_c = "IT000005",
        //                    BPI_CreateUser_c = CleanValue(Label4.Text),
        //                    BPI_CreateTime_c = CleanValue(row4.Cells[16].Text),
        //                    BPI_BinFrom_c = CleanValue(row4.Cells[5].Text),
        //                    BPI_WarehouseFrom_c = CleanValue(row4.Cells[4].Text),
        //                    BPI_WareHouseTo_c = CleanValue(row4.Cells[6].Text),
        //                    BPI_BinTo_c = CleanValue(row4.Cells[7].Text),
        //                    BPI_LotNum_c = "LOT-00000120221231",
        //                    BPI_TranQty_c = "1",
        //                    ShortChar10 = CleanValue(row4.Cells[3].Text),
        //                    CheckBox02 = true
        //                };

        //                // สร้าง HttpClient ในแต่ละรอบ
        //                using (HttpClient client = new HttpClient())
        //                {
        //                    string token = Request.QueryString["token"];
        //                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //                    string apiUrl = "https://erp.bpi-concretepile.co.th/BPI_Live/api/v1/Ice.BO.UD15Svc/UD15s/";

        //                    // แปลง JSON object เป็น JSON string
        //                    string jsonData = JsonConvert.SerializeObject(requestData);
        //                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //                    // ทำ HTTP POST request ไปยัง API
        //                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        string responseBody = await response.Content.ReadAsStringAsync();
        //                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "successmes", "successmes('โอนย้าย Picking No" + TextBox10.Text + "เรียบร้อยแล้ว')", true);
        //                        Label8.Text = "1";
        //                        Label9.Text = "True";
        //                        foreach (GridViewRow row in GridView1.Rows)
        //                        {
        //                            LinkButton deleteButton = (LinkButton)row.FindControl("DeleteButton");
        //                            deleteButton.Visible = false;
        //                        }
        //                        //Response.Redirect(Request.Url.AbsoluteUri);
        //                    }
        //                    else if (response.StatusCode == HttpStatusCode.Conflict)
        //                    {
        //                        continue; // ข้ามการโพสต์ข้อมูลแถวนี้แล้วลงไปในการวนลูปถัดไปใน GridView4
        //                    }
        //                    else
        //                    {
        //                        string responseBody = await response.Content.ReadAsStringAsync();
        //                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", $"error('Failed to post data to Ud15. Response: {responseBody}')", true);
        //                        Label8.Text = "";
        //                        Label9.Text = "False";
        //                        return;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // ถ้าลบข้อมูลไม่สำเร็จ ให้แจ้ง error และ return
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error('การลบข้อมูลล้มเหลว')", true);
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Error: " + ex.Message);
        //    }
        //}


        private void UpdateColumnQty(string deletedPartNumber)
        {
            if (ViewState["GridData5"] != null && ViewState["GridData5"] is DataTable)
            {
                DataTable dtGrid5 = (DataTable)ViewState["GridData5"];
                DataTable dt = (DataTable)ViewState["GridData"];
                // Loop through each row in GridView5
                foreach (DataRow row in dtGrid5.Rows)
                {

                    if (row["Key2"].ToString() == deletedPartNumber)
                    {
                        // Decrease the ColumnQty value by 1
                        int currentQty = Convert.ToInt32(row["ColumnQty"]);
                        int Qty = Convert.ToInt32(Label3.Text);
                        if (currentQty > 0)
                        {
                            row["ColumnQty"] = currentQty - Qty;
                        }


                    }
                }

                // Rebind GridView5
                GridView5.DataSource = dtGrid5;
                GridView5.DataBind();
            }
        }
        private int CalculateNewQty(DataTable dt)
        {
            int newQtyValue = 0;
            foreach (DataRow row in dt.Rows)
            {
                newQtyValue += Convert.ToInt32(row["Qty"]);

            }
            return newQtyValue;
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                DataTable dt = ViewState["GridData"] as DataTable;

                GridViewRow row = GridView1.Rows[rowIndex];

                if (dt != null && dt.Rows.Count > rowIndex)
                {
                    string deletedPartNumber = row.Cells[1].Text; // Assuming the PartNumber is in the second column
                    string deletedQty = dt.Rows[rowIndex]["Qty"].ToString(); // Assuming "Qty" is the column name

                    // Set the deleted quantity value to Label3
                    Label3.Text = deletedQty;
                    // Delete the row from the DataTable
                    dt.Rows[rowIndex].Delete();
                    ViewState["GridData"] = dt;
                    UpdateColumnQty(deletedPartNumber);
                    // Calculate new quantity value
                    int newQtyValue = CalculateNewQty(dt);
                    TextBox14.Text = newQtyValue.ToString();

                    // Rebind GridView1
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    // Update ColumnQty in GridView5


                    string value1 = row.Cells[1].Text;
                    string value2 = Company.Text;
                    string value3 = TextBox10.Text;
                    string value4 = row.Cells[2].Text;
                    string value5 = row.Cells[3].Text;
                    string value6 = row.Cells[4].Text;

                    // Execute SQL Query to retrieve data from GridView2
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = @"SELECT DISTINCT UD03.Key1 ,UD03.Character01
                         FROM ice.ud03 AS UD03 
                         JOIN Ice.UD28 AS UD28 ON UD03.Character01 = UD28.ShortChar08 
                         WHERE UD03.Character01 = @Column2
                         AND UD03.Company = @Company
                         AND UD28.Key1 = @Key1
                         AND UD03.Key5 = @ColumnLot
                        GROUP BY UD03.Key1, UD03.Key2,UD03.Character01, UD03.Key5, UD03.Key4, UD28.ShortChar10, UD28.ShortChar09, UD03.Key3";

                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@Column2", value1); // Provide value1
                        command.Parameters.AddWithValue("@Company", value2); // Provide value2
                        command.Parameters.AddWithValue("@Key1", value3); // Provide value3
                        command.Parameters.AddWithValue("@ColumnLot", value4); // Provide value4


                        // Execute the command and process the results
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Create a DataTable to hold the results
                            DataTable dtResults = new DataTable();
                            dtResults.Load(reader);

                            // Bind the results to GridView2
                            GridView2.DataSource = dtResults;
                            GridView2.DataBind();

                            // Retrieve the DataTable containing data for GridView4 from ViewState
                            DataTable dt4 = ViewState["GridData3"] as DataTable;

                            // Compare data in GridView2 with GridView4 and delete matching rows
                            if (dt4 != null)
                            {
                                foreach (DataRow row2 in dtResults.Rows)
                                {
                                    string key1Value = row2["Key1"].ToString(); // Assuming "Key1" is the column name

                                    for (int i = dt4.Rows.Count - 1; i >= 0; i--)
                                    {
                                        string key4Value = dt4.Rows[i]["TextColumn"].ToString(); // Assuming "Key4" is the column name

                                        if (key1Value == key4Value)
                                        {
                                            dt4.Rows.RemoveAt(i);
                                        }
                                    }
                                }

                                // Rebind GridView4 after removing matching rows
                                GridView4.DataSource = dt4;
                                GridView4.DataBind();
                            }
                        }
                    }
                }
                TextBox1.Text = "";
            }
        }
    }
}