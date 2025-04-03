<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CUstomerShipQR._default" %>

<!DOCTYPE html>

<html lang="en">
<head>
<title>Customer Shipment By Barcode</title>

<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;700&display=swap" rel="stylesheet">
<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins">
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet">

<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
      <script type="text/javascript">
          function Showerror() {
              Swal.fire({
                  icon: 'error', // กำหนดไอคอนเป็นข้อผิดพลาด
                  title: 'ไม่มีข้อมูล', // ข้อความแสดงในป็อปอัพ
                  showConfirmButton: true, // แสดงปุ่มยืนยัน
                  confirmButtonColor: '#3085d6', // สีของปุ่มยืนยัน
                  confirmButtonText: 'OK' // ข้อความบนปุ่มยืนยัน
              });
          }
          function Showerrormax() {
              Swal.fire({
                  icon: 'error', // กำหนดไอคอนเป็นข้อผิดพลาด
                  title: 'เกินกำหนดครับ', // ข้อความแสดงในป็อปอัพ
                  showConfirmButton: true, // แสดงปุ่มยืนยัน
                  confirmButtonColor: '#3085d6', // สีของปุ่มยืนยัน
                  confirmButtonText: 'OK' // ข้อความบนปุ่มยืนยัน
              });
          }
          function Showerrordub() {
              Swal.fire({
                  icon: 'error', // กำหนดไอคอนเป็นข้อผิดพลาด
                  title: 'สแกนซ้ำครับ', // ข้อความแสดงในป็อปอัพ
                  showConfirmButton: true, // แสดงปุ่มยืนยัน
                  confirmButtonColor: '#3085d6', // สีของปุ่มยืนยัน
                  confirmButtonText: 'OK' // ข้อความบนปุ่มยืนยัน
              });
          }
          function error(message) {
              Swal.fire({
                  icon: 'error', // กำหนดไอคอนเป็นข้อผิดพลาด
                  title: message, // กำหนดหัวข้อของแจ้งเตือน
                  showConfirmButton: true, // แสดงปุ่มยืนยัน
                  confirmButtonColor: '#3085d6', // สีของปุ่มยืนยัน
                  confirmButtonText: 'OK', // ข้อความบนปุ่มยืนยัน
                  customClass: {
                      title: 'swal-title-custom', // คลาส CSS สำหรับหัวข้อ
                      content: 'swal-content-custom' // คลาส CSS สำหรับเนื้อหา
                  }
              });
          }
          function errorSUC(message) {
              Swal.fire({
                  icon: 'success', // กำหนดไอคอนเป็นข้อผิดพลาด
                  title: message, // กำหนดหัวข้อของแจ้งเตือน
                  showConfirmButton: true, // แสดงปุ่มยืนยัน
                  confirmButtonColor: '#3085d6', // สีของปุ่มยืนยัน
                  confirmButtonText: 'OK', // ข้อความบนปุ่มยืนยัน
                  customClass: {
                      title: 'swal-title-custom', // คลาส CSS สำหรับหัวข้อ
                      content: 'swal-content-custom' // คลาส CSS สำหรับเนื้อหา
                  }
              });
          }
          function success() {
              Swal.fire({
                  icon: 'success', // กำหนดไอคอนเป็นข้อผิดพลาด
                  title: 'บันทึกข้อมูลเรียบร้อยแล้ว', // กำหนดหัวข้อของแจ้งเตือน
                  showConfirmButton: true, // แสดงปุ่มยืนยัน
                  confirmButtonColor: '#3085d6', // สีของปุ่มยืนยัน
                  confirmButtonText: 'OK' // ข้อความบนปุ่มยืนยัน
              });
          }
          function successmes(message) {
              Swal.fire({
                  icon: 'success', // กำหนดไอคอนเป็นข้อผิดพลาด
                  title: message, // กำหนดหัวข้อของแจ้งเตือน
                  showConfirmButton: true, // แสดงปุ่มยืนยัน
                  confirmButtonColor: '#3085d6', // สีของปุ่มยืนยัน
                  confirmButtonText: 'OK' // ข้อความบนปุ่มยืนยัน
              });
          }
      </script>
<style>
      .swal-title-custom {
        font-size: 18px; /* ลดขนาดฟอนต์ของหัวข้อ */
    }
    .swal-content-custom {
        font-size: 12px; /* ลดขนาดฟอนต์ของเนื้อหา */
    }
    .full-width {
    width: 100%;
}
body,h1,h2,h3,h4,h5 {font-family: "Poppins", sans-serif}
body {font-size:16px;}
.w3-half img{margin-bottom:-6px;margin-top:16px;opacity:0.8;cursor:pointer}
.w3-half img:hover{opacity:1}
/* CSS สำหรับเนื้อหาหลัก */
/* เปลี่ยนสีเป็นสีเทา */
.w3-Back {
    background-color: #f2f2f2; /* สีพื้นหลังของ container */
    padding: 20px; /* ระยะห่างของข้อความภายใน container */
}
.w3-sidebar {
    background-color: #f2f2f2; /* เปลี่ยนสีเป็นสีเทา */
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2); /* เพิ่มเงา */
}

 .textbox-container {
        width: 100%;
    }
.responsive-textbox {
        width: 100%;
    }
   @media (max-width: 768px) {
        .responsive-textbox {
            width: 50%;
        }
         .textbox-container {
        width: 50%;
        margin-left: 20px;
            }
    }

 .delivery-date {
        font-size: 20px; /* ปรับขนาดตามที่ต้องการ */
    }
  .custom-calendar {
        width: 200px; /* กำหนดความกว้างของ Calendar */
        font-size: 14px; /* กำหนดขนาดของตัวอักษรใน Calendar */
    }
  @media screen and (max-width: 780px) {
      .display-4 {
      font-size: 24px; /* ปรับขนาดข้อความให้เล็กลง */
  }
    
            .w3-main {
                margin-left: 0; /* ยกเลิกการให้ขอบซ้าย */
                margin-right: 0; /* ยกเลิกการให้ขอบขวา */
            }

            /* ปรับขนาดข้อความในหัวข้อใหญ่ */
            .w3-jumbo {
                font-size: 24px;
            }

            /* ปรับขนาดข้อความในส่วน delivery-date */
            .delivery-date {
                font-size: 14px;
            }

            /* ปรับขนาดของ TextBox และ DropDownList */
            #TextBox5,
            #DropDownList2,
            #DropDownList3,
            #DropDownList4,
            
            #TextBox2,
            #TextBox1 {
                width: 100%; /* กว้างเต็มระบบ */
            }
            #TextBox3,
            #TextBox4
            {
                font-size: 14px;
            }
                #TextBox10 {
                    width: 100%;
                     font-size: 14px; /* ขนาดตัวอักษรที่เล็กลง */

                }

             #TextBox6,
             #TextBox7,
             #TextBox8
          
            {
                 width: 100%; /* กว้างเต็มระบบ */

            }
            /* ปรับขนาดของ GridView */
            #GridView1 {
                width: 100%; /* กว้างเต็มระบบ */
                font-size: 12px; /* ขนาดข้อความเล็กลง */
            }
        }
  @media screen and (min-width: 600px) {
      .display-4 {
            font-size: 24px; /* ปรับขนาดข้อความให้เล็กลง */
        }
      .table-responsive {
        width: 100%;
        margin-bottom: 15px;
        overflow-x: scroll;
        overflow-y: hidden;
        -ms-overflow-style: -ms-autohiding-scrollbar;
        border: 1px solid #dddddd;
        -webkit-overflow-scrolling: touch;
    }
      .table-responsive > .table {
        margin-bottom: 0;
    }

    .table-responsive > .table > thead > tr > th,
    .table-responsive > .table > tbody > tr > th,
    .table-responsive > .table > tfoot > tr > th,
    .table-responsive > .table > thead > tr > td,
    .table-responsive > .table > tbody > tr > td,
    .table-responsive > .table > tfoot > tr > td {
        white-space: nowrap;
    }
    /* ปรับขนาดของ TextBox และ DropDownList เมื่อหน้าจอมีขนาดใหญ่ขึ้น */
    #TextBox5,
    #DropDownList2,
    #TextBox9,
    #DropDownList4,
    #TextBox2,
    #TextBox1,
    #TextBox3,
    #TextBox4,
    #TextBox6,
    #TextBox7,
  
    #TextBox8 {
        width: 100%;
    }

    #Button1,
    #Button3,
    #Button2 {
                width: 25%;


    }
}
    .button-inline {
        display: inline-block;
        margin-right: 10px; /* ปรับระยะห่างระหว่างปุ่ม */
    }
  input[type="date"]::-webkit-calendar-picker-indicator {
    transform: scale(2); /* ปรับขนาดไอคอนใหญ่ขึ้น */
}
  .camera-input {
    padding-right: 30px; /* กำหนดระยะห่างด้านขวาของ TextBox เพื่อให้ไอคอนอยู่ใน TextBox */
}

.camera-icon {
    position: absolute; /* กำหนดให้ไอคอนอยู่ในตำแหน่งที่แน่นอนภายใน TextBox */
    right: 10px; /* ย้ายไอคอนไปทางขวาของ TextBox */
    top: 50%; /* จัดให้ไอคอนอยู่ตรงกลางด้านตัวอักษร */
    transform: translateY(-50%); /* จัดให้ไอคอนอยู่ตรงกลางด้านตัวอักษรตามแนวดิ่ง */
    color: gray; /* กำหนดสีของไอคอน */
    cursor: pointer; /* ทำให้เมาส์เป็นรูปแบบของลูกศรเมื่อชี้ที่ไอคอน */
}
    .ChildGrid {
        width: 2000px; /* ปรับขนาดความกว้างตามต้องการ เช่น 400px */
        font-size: 10px; /* ปรับขนาดตัวอักษร */
    }
    .ChildGrid th, .ChildGrid td {
        padding: 5px; /* ปรับระยะห่างของเซลล์ */
    }
.textbox-container {
    display: inline-block;
    position: relative;
}

.textbox-container .form-control {
    padding-right: 30px; /* ปรับขนาดของ textbox เพื่อให้ icon ไม่ทับข้อความ */
    border: 1px solid #ced4da; /* เป็นตัวอย่างเท่านั้น คุณสามารถปรับแต่งตามต้องการได้ */
    border-radius: .25rem; /* เป็นตัวอย่างเท่านั้น คุณสามารถปรับแต่งตามต้องการได้ */
}
  .textbox-container .fas.fa-qrcode {
    position: absolute;
    top: 50%;
    right: 5px;
    transform: translateY(-50%);
    font-size: 16px; /* ขนาดของไอคอน */
    color: #495057; /* สีของไอคอน */
}
.textbox-container .fas.fa-camera {
    position: absolute;
    top: 50%;
    right: 5px;
    transform: translateY(-50%);
    font-size: 16px; /* ขนาดของไอคอน */
    color: #495057; /* สีของไอคอน */
}
 #GridView1 tr {
        text-align: center;
    }
     .camera-container {
        margin-top: 20px;
        position: relative;
    }

    video, canvas {
        border: 1px solid black;
        margin-top: 10px;
    }

    #start-camera, #capture-photo {
        margin-top: 10px;
    }
 #preview {
     width: 100%;
    height: 300px; /* ปรับความสูงของวิดีโอตามที่ต้องการ */
    object-fit: contain; 
    transform: scaleX(-2); /* กลับด้านของวิดีโอให้สะท้อน */
}
  .hideColumn {
        display: none;
    }
  .hiddenColumn {
    display: none;
}
          .popup-someEntity{width: 500px;}
  .w3-main {
        margin: auto;
    }
  .table.table-striped.full-width td {
        font-size: 20px; /* ปรับขนาดตัวอักษรตามที่คุณต้องการ */
    }
  .username-label-text {
    font-size: 12px; /* Adjust the font size as needed */
    color: #ffffff; /* Optional: Set the text color to white to make it stand out on the black background */
}
  .username-label {
    float: right; /* Aligns the label to the right */
}
      .fixed-header {
    position: sticky;
    top: 0;
    z-index: 1;
    background-color: #fff; /* สีพื้นหลังของหัวตาราง */

}
</style>

</head>
<body>

<!-- ส่วน Navbar -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <span class="navbar-brand" href="#">BPI</span>

      
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                    <a class="nav-link" href="https://webapp.bpi-concretepile.co.th:8080/">Home <span class="sr-only">(current)</span></a>
                </li>
            </ul>
        </div>
          <div class="mr-auto">
        <span class="navbar-brand">Username: <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></span>

    </div>
    </nav>

<!-- Top menu on small screens -->
<header class="w3-bar w3-top w3-hide-large w3-black w3-xlarge">
    <a class="nav-link" href="https://webapp.bpi-concretepile.co.th:8080/">Home <span class="sr-only">(current)</span></a>
</header>

<!-- Overlay effect when opening sidebar on small screens -->
    <asp:Label ID="Label5" runat="server" Text="" Visible ="false" ></asp:Label><br/>
     <asp:Label ID="Label6" runat="server" Text="" Visible ="false" ></asp:Label>
<div class="overlay d-lg-none" onclick="w3_close()" title="close side menu" id="myOverlay"></div>

<!-- !PAGE CONTENT! -->
   <div class="w3-main" style="margin: auto;">
   <!-- Modal for video popup -->

    <div id="videoModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="videoModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="videoModalLabel">Scan Barcode</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <video id="video" style="width: 100%; max-width: 640px; height: auto; margin-top: 10px;"></video>
                    <canvas id="canvas" style="display:none;"></canvas>
                </div>
            </div>
        </div>
    </div>


<!-- Vertically centered modal -->
<div class="modal fade" id="scanModal" tabindex="-1" role="dialog" aria-labelledby="scanModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-body">
                <div id="reader" class="w-100" style="max-width: 100%;"></div>
                <button type="button" class="btn btn-secondary mt-3" data-bs-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
       <!-- Vertically centered modal -->
<div class="modal fade" id="scanModal2" tabindex="-1" role="dialog" aria-labelledby="scanModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-body">
                <div id="reader2" class="w-100" style="max-width: 100%;"></div>
                <button type="button" class="btn btn-secondary mt-3" data-bs-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<div class="w3-main" style="margin: auto; ">

  <!-- Header -->
        <form runat="server">
<asp:HiddenField ID="hiddenFieldTargetControl" runat="server" />
       <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="True" CssClass="ChildGrid" Visible="true">
    <Columns>
    </Columns>
</asp:GridView>
              <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid" Visible="false">
      <Columns>

      </Columns>
       </asp:GridView>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="true" Visible="false">
                <Columns>
      

     
    </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="true" Visible="false">
                            <Columns>
  

 
</Columns>
            </asp:GridView>
   
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="True" CssClass="ChildGrid" style="display:none">
       <Columns>
       </Columns>
        </asp:GridView> 
                 <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="True" CssClass="ChildGrid" Visible="false">
<Columns>
</Columns>
 </asp:GridView> 
            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="false" Visible="false">
            <Columns>
    
   <asp:BoundField HeaderText="Key2" DataField="Key2" />
  <asp:BoundField HeaderText="ColumnQty" DataField="ColumnQty" />
                <asp:BoundField HeaderText="Number04" DataField="Number04" />
</Columns>
        </asp:GridView>
<h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

  <div class="w3-Back" style="margin-top:0px" id="showcase">

    <h1 class="display-4">โปรแกรมยิงเข็มหน้าป้อม  </h1>

      <div class="card">    

      <div class="card-body">
               <h1 class="delivery-date w3-text-Black">
     <b>บริษัท:</b>
 <asp:TextBox ID="Company" runat="server" class="form-control responsive-textbox" ReadOnly="True" style="display: inline-block; margin-left: 10px;"></asp:TextBox>
 </h1>
      
      <div class="form-group">
    <h1 class="delivery-date w3-text-Black">
        <b>วันที่ส่งสินค้า:</b>
        <asp:Label ID="Part" runat="server" Text="Label" Visible="false"></asp:Label>
        <div class="textbox-container" style="position: relative; display: inline-block; margin-left: 1px;">
            <asp:TextBox ID="TextBox5" runat="server" class="form-control responsive-textbox" type="text" AutoPostBack="True"></asp:TextBox>
            <i id="calendarIcon" class="far fa-calendar-alt" style="position: absolute; top: 50%; right: 10px; transform: translateY(-50%); cursor: pointer;"></i>
        </div>
    </h1>
<h1 class="delivery-date w3-text-Black">

    <b>ใบขึ้นของ:</b>     
              <asp:Label ID="SupplierName" runat="server" Text="asdasds" Visible ="false"></asp:Label>
    <asp:Label ID="Car" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="Release" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="UOM" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="sent" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="starttime" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="endtime" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="cusid" runat="server" Text=" " Visible ="false"></asp:Label>
    <asp:Label ID="shipnum" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="pack_so" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="typecar" runat="server" Text="" Visible ="false"></asp:Label>
    <asp:Label ID="Label8" runat="server" Text="" Visible ="True"></asp:Label>
    <asp:Label ID="sumud30" runat="server" Text="" style="display:none;"></asp:Label>

            <asp:TextBox ID="TextBox10" runat="server" class="form-control" ReadOnly="false" AutoPostBack="True" OnTextChanged="TextBox10_TextChanged"></asp:TextBox>

        <div class="textbox-container" style="position: relative; display: inline-block; ">

<i id="cameraIcon" class="fas fa-qrcode" style="font-size: 24px; pointer-events: none; display:none;"></i>

    </div>

</h1>
       </div>
<h1 class="delivery-date w3-text-Black"><b>ชื่อหน่วยงาน:</b> <asp:TextBox ID="Compname" runat="server" class="form-control" ReadOnly="true"  Visible="False"></asp:TextBox>  </h1>
          <h1 class="delivery-date w3-text-Black"><b>   </b> <asp:TextBox ID="TextBox4" runat="server" class="form-control" readonly="true" ></asp:TextBox></h1>
 <h1 class="delivery-date w3-text-Black"><b></b> <asp:TextBox ID="TextBox2" runat="server" class="form-control" readonly="true" Visible="False" ></asp:TextBox></h1>
            <h1 class="delivery-date w3-text-Black"><b>ทะเบียนรถ :</b> <asp:TextBox ID="Carid" runat="server" class="form-control" readonly="true" ></asp:TextBox>
<h1 class="delivery-date w3-text-Black">
    
</h1>
        
    <h1 class="delivery-date w3-text-Black"><b></b> </h1>
           
           <h1 class="delivery-date w3-text-Black">
                          <div style="display: flex;">
    <div class="delivery-container" >
        <h1 class="delivery-date w3-text-Black"><b>ต้องการส่ง:</b>
      <asp:TextBox ID="TextBox13" runat="server" class="form-control" ReadOnly="true" Width="30%"></asp:TextBox>

        </h1>
    </div>
    <div style="flex: 1000;">
        <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="QTYBYPart" runat="server" Text="Label" style="display:none"></asp:Label>

    </div>
    <div style="flex: 50;">
        <h1 class="delivery-date w3-text-Black"><b>จำนวน:</b><asp:Label ID="Label1" runat="server" Text="Low value" Visible="False"></asp:Label>
            <asp:TextBox ID="TextBox14" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>

        </h1>
    </div>
    <div style="flex: 1;">
    </div>
    <div style="flex: 20;">
        <h1 class="delivery-date w3-text-Black"><b></b>
                    

        </h1>
    </div>
</div>
                               
                         <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
    <h1 class="delivery-date w3-text-Black"><b id="listin" style="display:block"> รายการ:</b>
       <asp:Label ID="Label9" runat="server" Text="" Visible="True" Style="font-size: 0px;"></asp:Label>
               <asp:Label ID="Label11" runat="server" Text="" Visible="True" Style="font-size: 0px;"></asp:Label>

<asp:Label ID="Label10" runat="server" Text="" Visible="True" Style="font-size: 0px;"></asp:Label>
                <div id ="Gridin" style="display:block">

<div class="table-responsive" style="max-height: 300px; overflow-x: auto; overflow-y: auto;">

    <asp:GridView ID="GridView8" CssClass="table table-striped full-width" runat="server" AutoGenerateColumns="False" Style="max-width: 100%; overflow-x: auto; overflow-y: auto; max-height: 300px;" OnRowCommand="GridView1_RowCommand">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    <FooterStyle BackColor="Tan" />
        <HeaderStyle CssClass="fixed-header" BackColor="Tan" Font-Bold="True" />
    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
    <SortedAscendingCellStyle BackColor="#FAFAE7" />
    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
    <SortedDescendingCellStyle BackColor="#E1DB9C" />
    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    <Columns>
     
        <asp:TemplateField HeaderText="Seq">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
    </ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="PartNumber" DataField="PartNumber" HeaderStyle-CssClass="hiddenColumn" ItemStyle-CssClass="hiddenColumn" />
                       <asp:BoundField HeaderText="Part Description" DataField="Description" />

                <asp:BoundField HeaderText="LOT" DataField="LOT" visible="false" />
         <asp:BoundField HeaderText="Warehouse" DataField="Warehouse"  visible="true"/>
        <asp:BoundField HeaderText="Bin" DataField="Bin" visible="true" />
        <asp:BoundField HeaderText="Valuescan" DataField="Valuescan" visible="false"/>
        <asp:BoundField HeaderText="Qty"  DataField="QTY" />
                        <asp:TemplateField HeaderText="Delete" visible="false">
    <ItemTemplate>
    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn btn-danger" CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('คุณแน่ใจหรือไม่ว่าต้องการลบรายการนี้?');">
    <i class="fas fa-trash-alt"></i>
</asp:LinkButton>
    </ItemTemplate>
   </asp:TemplateField>
    </Columns>
</asp:GridView>
        
            </div>
                    </div>
           <b id="Barcode" style="flex-grow: 1;display:none">ใบขึ้นของ  :    <asp:Label ID="Label7" runat="server" Text="" Visible ="true"></asp:Label>
</b>
     </h1>
           <h1 class="delivery-date w3-text-Black" style="display: flex; align-items: center;">
    <div class="textbox-container" style="position: relative; display: inline-block; width: 100%;">

        <asp:TextBox ID="TextBox1" runat="server" class="form-control" ReadOnly="False" Style="margin-left: 5px; padding-right: 30px;display:none; width: 100%;" OnTextChanged="TextBox1_TextChanged" AutoPostBack="True"></asp:TextBox>
<i id="Camerabarcode" class="fas fa-camera" style="display:none; position: absolute; top: 50%; right: 5px; transform: translateY(-50%); font-size: 24px; pointer-events: none;"></i>
    </div>
</h1>
         
          <asp:HiddenField ID="HiddenFieldIndex" runat="server" Value="" />
    <h1 class="delivery-date w3-text-Black"><b id="list" style="display:none"> รายการ:         </b>
                                       <div id ="GridSave" style="display:block">

<div class="table-responsive" style="max-height: 300px; overflow-x: auto; overflow-y: auto;">

    <asp:GridView ID="GridView10" CssClass="table table-striped full-width" runat="server" AutoGenerateColumns="False" Style="max-width: 100%; overflow-x: auto; overflow-y: auto; max-height: 300px;" OnRowCommand="GridView1_RowCommand">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    <FooterStyle BackColor="Tan" />
        <HeaderStyle CssClass="fixed-header" BackColor="Tan" Font-Bold="True" />
    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
    <SortedAscendingCellStyle BackColor="#FAFAE7" />
    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
    <SortedDescendingCellStyle BackColor="#E1DB9C" />
    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    <Columns>
     
        <asp:TemplateField HeaderText="Seq">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
    </ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="PartNumber" DataField="PartNumber" HeaderStyle-CssClass="hiddenColumn" ItemStyle-CssClass="hiddenColumn" />
                       <asp:BoundField HeaderText="Part Description" DataField="Description" />

                <asp:BoundField HeaderText="LOT" DataField="LOT" visible="false" />
         <asp:BoundField HeaderText="Warehouse" DataField="Warehouse"  visible="true"/>
        <asp:BoundField HeaderText="Bin" DataField="Bin" visible="true" />
        <asp:BoundField HeaderText="Valuescan" DataField="Valuescan" visible="false"/>
        <asp:BoundField HeaderText="Qty"  DataField="QTY" />
                        <asp:TemplateField HeaderText="Delete" visible="false">
    <ItemTemplate>
    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn btn-danger" CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('คุณแน่ใจหรือไม่ว่าต้องการลบรายการนี้?');">
    <i class="fas fa-trash-alt"></i>
</asp:LinkButton>
    </ItemTemplate>
   </asp:TemplateField>
    </Columns>
</asp:GridView>
            </div>
                    </div>
        <div id ="GridCustomer" style="display:none">
<div class="table-responsive" style="max-height: 300px; overflow-x: auto; overflow-y: auto;">

    <asp:GridView ID="GridView1" CssClass="table table-striped full-width" runat="server" AutoGenerateColumns="False" Style="max-width: 100%; overflow-x: auto; overflow-y: auto; max-height: 300px;" OnRowCommand="GridView1_RowCommand">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    <FooterStyle BackColor="Tan" />
        <HeaderStyle CssClass="fixed-header" BackColor="Tan" Font-Bold="True" />
    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
    <SortedAscendingCellStyle BackColor="#FAFAE7" />
    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
    <SortedDescendingCellStyle BackColor="#E1DB9C" />
    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    <Columns>
     
        <asp:TemplateField HeaderText="Seq">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
    </ItemTemplate>
</asp:TemplateField>
       <asp:BoundField HeaderText="PartNumber" DataField="PartNumber" visible ="false"/>
                       <asp:BoundField HeaderText="Description" DataField="Description" />

                <asp:BoundField HeaderText="LOT" DataField="LOT" visible="false" />
         <asp:BoundField HeaderText="Warehouse" DataField="Warehouse"  visible="true"/>
        <asp:BoundField HeaderText="Bin" DataField="Bin" visible="true" />
        <asp:BoundField HeaderText="Valuescan" DataField="Valuescan" visible="false"/>
        <asp:BoundField HeaderText="Qty"  DataField="QTY" />
                        <asp:TemplateField HeaderText="Delete" visible="false">
    <ItemTemplate>
    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn btn-danger" CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('คุณแน่ใจหรือไม่ว่าต้องการลบรายการนี้?');">
    <i class="fas fa-trash-alt"></i>
</asp:LinkButton>
    </ItemTemplate>
   </asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>
            </div>
                        
    </h1>

<div style="display: flex; gap: 10px;">
    <asp:Button ID="Button2" runat="server" Text="โอนย้าย" CssClass="btn btn-primary" UseSubmitBehavior="False" OnClick="Button2_Click1"     OnClientClick="return confirmTransfer();" style="display:block" />

    <asp:Button ID="Button3" runat="server" Text="ยืนยัน" CssClass="btn btn-success" UseSubmitBehavior="False" OnClick="Button3_Click1" style="display:none" />

    <asp:Button ID="Button1" runat="server" Text="ยกเลิก" CssClass="btn btn-danger" UseSubmitBehavior="False" OnClick="Button1_Click" OnClientClick="return confirmCancel();"/>
</div>

<asp:Button ID="Button4" runat="server" Text="แสดงข้อมูลทั้งหมด" CssClass="btn btn-primary" OnClientClick="showModal(event); return false;" UseSubmitBehavior="false" />

            </h1>
          <asp:HiddenField ID="HiddenRowIndex" runat="server" />

</div>

  </div>

  </form>
    <!-- Bootstrap Modal -->
<div class="modal fade" id="serialModal" tabindex="-1" role="dialog" aria-labelledby="serialModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="serialModalLabel">Serial ทั้งหมด</h5>
                <button type="button" class="close" id="closeModalButton" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <!-- ตารางที่จะใช้แสดงข้อมูล -->
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Serial</th>
                        </tr>
                    </thead>
                    <tbody id="gridViewData">
                        <!-- ข้อมูลจาก GridView4 จะถูกเพิ่มที่นี่ -->
                    </tbody>
                </table>
            </div>
            <div class="modal-footer">
                <!-- ปุ่มปิด modal -->
                <button type="button" class="btn btn-secondary" id="closeModalFooterButton">ปิด</button>
            </div>
        </div>
    </div>
</div>
  <!-- Photo grid (modal) -->
<%--   <audio id="successSound" src="C:\Users\USER069\source\repos\CustomerShip\CustomerShip\Sound\prompt-user-for-response-85808.mp3"></audio>--%>

  <!-- Modal for full size images on click-->
  <div id="modal01" class="w3-modal w3-black" style="padding-top:0" onclick="this.style.display='none'">
    <span class="w3-button w3-black w3-xxlarge w3-display-topright">×</span>
    <div class="w3-modal-content w3-animate-zoom w3-center w3-transparent w3-padding-64">
      <img id="img01" class="w3-image">
      <p id="caption"></p>
    </div>
  </div>

    <!-- End page content -->
     <footer style="text-align: left; padding: 10px; background-color: #f1f1f1; position: relative; bottom: 0; width: 100%;">
    🅥2.1.3 DevBy : ⒾⓉ ⒷⓟⒾ ⓉⒺⒶⓂ
</footer>

<script>
    // Script to open and close sidebar
    function w3_open() {
        document.getElementById("mySidebar").style.display = "block";
        document.getElementById("myOverlay").style.display = "block";
    }

    function w3_close() {
        document.getElementById("mySidebar").style.display = "none";
        document.getElementById("myOverlay").style.display = "none";
    }

    // Modal Image Gallery
    function onClick(element) {
        document.getElementById("img01").src = element.src;
        document.getElementById("modal01").style.display = "block";
        var captionText = document.getElementById("caption");
        captionText.innerHTML = element.alt;
    }
    function playSuccessSound() {
        var successAudio = document.getElementById('successSound');
        successAudio.play();
    }
</script>
 
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
 <script type="text/javascript"src="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/7.3.0/mdb.umd.min.js"></script>
 <!-- Include Bootstrap JS and Html5Qrcode library -->
<%--    <script src="https://unpkg.com/html5-qrcode" type="text/javascript"></script>--%>
 <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<%-- <script src="https://unpkg.com/html5-qrcode/minified/html5-qrcode.min.js"></script> --%>
    <script>
        function showModal(event) {
            // ป้องกันการรีเฟรชหน้าจอถ้าถูกเรียกจากการกดปุ่ม
            if (event) {
                event.preventDefault();
            }

            // ดึงข้อมูลจาก GridView4
        var gridView = document.getElementById('<%= GridView4.ClientID %>');
        var rows = gridView.getElementsByTagName('tr');
        var tableBody = document.getElementById('gridViewData');

        // ลบข้อมูลเก่าทั้งหมดใน tbody ของตาราง modal
        tableBody.innerHTML = '';

        // ตรวจสอบว่า GridView4 มีข้อมูลหรือไม่
        if (rows.length <= 1) { // ถ้าไม่มีข้อมูล (header อย่างเดียว)
            Swal.fire({
                icon: 'warning',
                title: 'ไม่มีข้อมูล',
                text: 'ใบนี้ยังไม่มีข้อมูล',
                confirmButtonText: 'ตกลง'
            });
            return; // ออกจากฟังก์ชัน
        }

        // ลูปผ่านข้อมูลแต่ละแถวใน GridView4 แล้วนำมาแสดงใน Modal
        for (var i = 1; i < rows.length; i++) { // เริ่มที่ 1 เพื่อข้าม header ของ GridView
            var cells = rows[i].getElementsByTagName('td');
            if (cells.length > 0) {
                var newRow = document.createElement('tr');
                var newCell = document.createElement('td');
                newCell.textContent = cells[0].textContent; // นำข้อมูลจากคอลัมน์แรก (TextColumn)
                newRow.appendChild(newCell);
                tableBody.appendChild(newRow);
            }
        }

        // แสดง modal
        $('#serialModal').modal('show');
    }
    </script>


    <script>
        // ฟังก์ชันเพื่อปิด modal
        function closeModal() {
            $('#serialModal').modal('hide');
        }

        // เพิ่ม event listener ให้กับปุ่มปิดใน footer
        $('#closeModalFooterButton').on('click', closeModal);

        // เพิ่ม event listener ให้กับปุ่ม "X"
        $('#closeModalButton').on('click', closeModal);
</script>
<%--    <script>
        document.addEventListener("DOMContentLoaded", async function () {
            const html5QrCode = new Html5Qrcode("reader");
            const qrCodeResultInput = document.getElementById("TextBox10");
            const startScanBtn = document.getElementById("cameraIcon");
            const scanModal = new bootstrap.Modal(document.getElementById('scanModal'));
            var Sound = new Audio("Sound/store-scanner-beep-90395.mp3");
            function onScanSuccess(decodedText, decodedResult) {
                qrCodeResultInput.value = decodedText;
                console.log("Scanned QR Code:", decodedText);
                Sound.play(); // เล่นเสียงสำหรับสำเร็จ

                html5QrCode.stop().then(() => {
                    scanModal.hide();
                    // ส่งอีเวนต์ keypress ให้กับ TextBox เพื่อจำลองการกดปุ่ม Enter
                    const enterKeyEvent = new KeyboardEvent("keypress", {
                        key: "Enter",
                        code: "Enter",
                        keyCode: 13,
                        which: 13,
                        bubbles: true,
                        cancelable: true,
                    });
                    qrCodeResultInput.dispatchEvent(enterKeyEvent);
                }).catch(err => console.error("Error stopping QR code scanner:", err));
                document.getElementById('TextBox10').dispatchEvent(enterKeyEvent);

            }

            function onScanError(errorMessage) {
                console.error("Error scanning QR code:", errorMessage);
                playSuccessSound(); // เล่นเสียงสำหรับสำเร็จ

            }

            startScanBtn.addEventListener("click", function () {
                scanModal.show();
                scanModal._element.addEventListener('shown.bs.modal', function () {
                    html5QrCode.start({ facingMode: "environment" }, { fps: 30, qrbox: 250, aspectRatio: 2.0 }, onScanSuccess, onScanError);
                }, { once: true });
            });

            scanModal._element.addEventListener('hidden.bs.modal', function () {
                html5QrCode.stop().catch(err => console.error("Error stopping QR code scanner:", err));
            });
        });
    </script>
  

    <script>
        document.addEventListener("DOMContentLoaded", async function () {
            const html5QrCode = new Html5Qrcode("reader");
            const qrCodeResultInput = document.getElementById("TextBox1");
            const startScanBtn = document.getElementById("Camerabarcode");
            const scanModal = new bootstrap.Modal(document.getElementById('scanModal'));
            var Sound = new Audio("Sound/store-scanner-beep-90395.mp3");
            function onScanSuccess(decodedText, decodedResult) {
                qrCodeResultInput.value = decodedText;
                console.log("Scanned QR Code:", decodedText);
                Sound.play(); // เล่นเสียงสำหรับสำเร็จ

                html5QrCode.stop().then(() => {
                    scanModal.hide();
                    // ส่งอีเวนต์ keypress ให้กับ TextBox เพื่อจำลองการกดปุ่ม Enter
                    const enterKeyEvent = new KeyboardEvent("keypress", {
                        key: "Enter",
                        code: "Enter",
                        keyCode: 13,
                        which: 13,
                        bubbles: true,
                        cancelable: true,
                    });
                    qrCodeResultInput.dispatchEvent(enterKeyEvent);
                }).catch(err => console.error("Error stopping QR code scanner:", err));
                document.getElementById('TextBox1').dispatchEvent(enterKeyEvent);

            }

            function onScanError(errorMessage) {
                console.error("Error scanning QR code:", errorMessage);
                playSuccessSound(); // เล่นเสียงสำหรับสำเร็จ

            }

            startScanBtn.addEventListener("click", function () {
                scanModal.show();
                scanModal._element.addEventListener('shown.bs.modal', function () {
                    html5QrCode.start({ facingMode: "environment" }, { fps: 30, qrbox: 100, aspectRatio: 2.0 }, onScanSuccess, onScanError);
                }, { once: true });
            });

            scanModal._element.addEventListener('hidden.bs.modal', function () {
                html5QrCode.stop().catch(err => console.error("Error stopping QR code scanner:", err));
            });
        });
    </script>--%>
 
<%--<script>
    let scanner; // ประกาศตัวแปรเพื่อใช้งาน Instascan
    // เมื่อคลิกที่ไอคอนกล้องสำหรับสแกนบาร์โค้ด
    document.getElementById('cameraIcon').addEventListener('click', function () {
        // เปิดโมดัล video
        $('#videoModal').modal('show');

        // เริ่มการสแกน
        scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
        scanner.addListener('scan', function (content) {
            alert('Content: ' + content); // แสดงข้อมูลที่สแกนได้ในรูปแบบของ Alert
            // นำข้อมูลที่ได้ไปใส่ใน TextBox10
            document.getElementById('TextBox10').value = content;
            // สร้าง event สำหรับกดปุ่ม Enter
            var enterKeyEvent = new KeyboardEvent('keypress', {
                key: 'Enter',
                code: 'Enter',
                keyCode: 13,
                which: 13,
                bubbles: true,
                cancelable: true
            });

            // ส่ง event ไปยัง TextBox10
            document.getElementById('TextBox10').dispatchEvent(enterKeyEvent);
            // ปิดกล้องเมื่อสแกนเสร็จ
            scanner.stop();
            // ปิดโมดัล video
            $('#videoModal').modal('hide');
        });

        Instascan.Camera.getCameras().then(function (cameras) {
            // ตรวจสอบว่ามีกล้องหลังที่พร้อมใช้งานหรือไม่
            const backCamera = cameras.find(camera => camera.name.toLowerCase().includes('back'));

            if (backCamera) {
                // เริ่มการสแกนด้วยกล้องหลัง
                scanner.start(backCamera);
                document.getElementById('preview').style.transform = 'rotate(360deg)';

            } else if (cameras.length > 0) {
                scanner.start(cameras[0]); // เริ่มการสแกนโดยใช้กล้องแรก (อาจเป็นกล้องหน้าหรือกล้องหลัง ขึ้นอยู่กับตำแหน่งในอาร์เรย์)

                console.error('No back camera found.');
                alert('No back camera found.');
            }
        }).catch(function (e) {
            console.error(e);
        });
    

    });

    // เมื่อคลิกที่ไอคอนกล้องสำหรับสแกนบาร์โค้ด
    document.getElementById('Camerabarcode').addEventListener('click', function () {
        // เปิดโมดัล video
        $('#videoModal').modal('show');

        // เริ่มการสแกน
        scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
        scanner.addListener('scan', function (content) {
            alert('Content: ' + content); // แสดงข้อมูลที่สแกนได้ในรูปแบบของ Alert
            // นำข้อมูลที่ได้ไปใส่ใน TextBox10
            document.getElementById('TextBox1').value = content;
            // สร้าง event สำหรับกดปุ่ม Enter
            var enterKeyEvent = new KeyboardEvent('keypress', {
                key: 'Enter',
                code: 'Enter',
                keyCode: 13,
                which: 13,
                bubbles: true,
                cancelable: true
            });

            // ส่ง event ไปยัง TextBox10
            document.getElementById('TextBox1').dispatchEvent(enterKeyEvent);
            // ปิดกล้องเมื่อสแกนเสร็จ
            scanner.stop();
            // ปิดโมดัล video
            $('#videoModal').modal('hide');
        });

        Instascan.Camera.getCameras().then(function (cameras) {
            // ตรวจสอบว่ามีกล้องหลังที่พร้อมใช้งานหรือไม่
            const backCamera = cameras.find(camera => camera.name.toLowerCase().includes('back'));

            if (backCamera) {
                // เริ่มการสแกนด้วยกล้องหลัง
                scanner.start(backCamera);
                document.getElementById('preview').style.transform = 'scaleX(-1)';

            } else if (cameras.length > 0) {
                scanner.start(cameras[0]); // เริ่มการสแกนโดยใช้กล้องแรก (อาจเป็นกล้องหน้าหรือกล้องหลัง ขึ้นอยู่กับตำแหน่งในอาร์เรย์)
                console.error('No back camera found.');
                alert('No back camera found.');
            }
        }).catch(function (e) {
            console.error(e);
        });
    });
    // เมื่อคลิกที่ไอคอนกล้องสำหรับสแกนบาร์โค้ด
    document.getElementById('Warehousecamera').addEventListener('click', function () {
        // เปิดโมดัล video
        $('#videoModal').modal('show');

        // เริ่มการสแกน
        scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
        scanner.addListener('scan', function (content) {
            alert('Content: ' + content); // แสดงข้อมูลที่สแกนได้ในรูปแบบของ Alert
            // นำข้อมูลที่ได้ไปใส่ใน TextBox10
            document.getElementById('Warehouse').value = content;
            // สร้าง event สำหรับกดปุ่ม Enter
            var enterKeyEvent = new KeyboardEvent('keypress', {
                key: 'Enter',
                code: 'Enter',
                keyCode: 13,
                which: 13,
                bubbles: true,
                cancelable: true
            });

            // ส่ง event ไปยัง TextBox10
            document.getElementById('Warehouse').dispatchEvent(enterKeyEvent);
            // ปิดกล้องเมื่อสแกนเสร็จ
            scanner.stop();
            // ปิดโมดัล video
            $('#videoModal').modal('hide');
        });

        Instascan.Camera.getCameras().then(function (cameras) {
            // ตรวจสอบว่ามีกล้องหลังที่พร้อมใช้งานหรือไม่
            const backCamera = cameras.find(camera => camera.name.toLowerCase().includes('back'));

            if (backCamera) {
                // เริ่มการสแกนด้วยกล้องหลัง
                scanner.start(backCamera);
                document.getElementById('preview').style.transform = 'scaleX(-1)';

            } else if (cameras.length > 0) {
                scanner.start(cameras[0]); // เริ่มการสแกนโดยใช้กล้องแรก (อาจเป็นกล้องหน้าหรือกล้องหลัง ขึ้นอยู่กับตำแหน่งในอาร์เรย์)
                console.error('No back camera found.');
                alert('No back camera found.');
            }
        }).catch(function (e) {
            console.error(e);
        });
    });

    // เมื่อคลิกที่ไอคอนกล้องสำหรับสแกนบาร์โค้ด
    document.getElementById('BinCmera').addEventListener('click', function () {
        // เปิดโมดัล video
        $('#videoModal').modal('show');

        // เริ่มการสแกน
        scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
        scanner.addListener('scan', function (content) {
            alert('Content: ' + content); // แสดงข้อมูลที่สแกนได้ในรูปแบบของ Alert
            // นำข้อมูลที่ได้ไปใส่ใน TextBox10
            document.getElementById('Bin').value = content;
            // สร้าง event สำหรับกดปุ่ม Enter
            var enterKeyEvent = new KeyboardEvent('keypress', {
                key: 'Enter',
                code: 'Enter',
                keyCode: 13,
                which: 13,
                bubbles: true,
                cancelable: true
            });

            // ส่ง event ไปยัง TextBox10
            document.getElementById('Bin').dispatchEvent(enterKeyEvent);
            // ปิดกล้องเมื่อสแกนเสร็จ
            scanner.stop();
            // ปิดโมดัล video
            $('#videoModal').modal('hide');
        });

        Instascan.Camera.getCameras().then(function (cameras) {
            // ตรวจสอบว่ามีกล้องหลังที่พร้อมใช้งานหรือไม่
            const backCamera = cameras.find(camera => camera.name.toLowerCase().includes('back'));

            if (backCamera) {
                // เริ่มการสแกนด้วยกล้องหลัง
                scanner.start(backCamera);
                document.getElementById('preview').style.transform = 'scaleX(-1)';

            } else if (cameras.length > 0) {
                scanner.start(cameras[0]); // เริ่มการสแกนโดยใช้กล้องแรก (อาจเป็นกล้องหน้าหรือกล้องหลัง ขึ้นอยู่กับตำแหน่งในอาร์เรย์)
                console.error('No back camera found.');
                alert('No back camera found.');
            }
        }).catch(function (e) {
            console.error(e);
        });
    });

</script>--%>
<script>
    function DeleteButton() {
        window.open('popupPage.aspx', 'ชื่อหน้าต่าง', 'width=500,height=400');
    }
</script>
    <script>
        $(function () {
            $("#TextBox5").datepicker({
                dateFormat: 'dd/mm/yy', // รูปแบบวันที่ที่ต้องการ
                onSelect: function (dateText, inst) {
                    $(this).val(dateText); // ตั้งค่าค่าวันที่ที่เลือกใน TextBox
                }
            });
            $("#calendarIcon").click(function (event) {
                event.stopPropagation();
                $("#TextBox5").datepicker({
                    dateFormat: 'dd/mm/yy', // รูปแบบวันที่ที่ต้องการ
                    onSelect: function (dateText, inst) {
                        $("#TextBox5").val(dateText); // ตั้งค่าค่าวันที่ที่เลือกใน TextBox
                    }
                }).datepicker("show");
            });

            // ป้องกันเหตุการณ์ click ที่ TextBox5
            $("#TextBox5").click(function (event) {
                event.stopPropagation();
            });
        });
    </script>

    <script type="text/javascript">
        window.onload = function () {
            var label7 = document.getElementById('Label7');

            var label7Content = label7 ? label7.textContent.trim() : '';
            var textBox1 = document.getElementById('<%= TextBox1.ClientID %>');

            if (label7Content) {
                // มีข้อมูลใน Label7
                document.getElementById('Barcode').style.display = 'block';
                document.getElementById('Camerabarcode').style.display = 'block';
                document.getElementById('TextBox1').style.display = 'block';
                document.getElementById('GridCustomer').style.display = 'block';
                document.getElementById('list').style.display = 'block';
                document.getElementById('Gridin').style.display = 'none';
                document.getElementById('listin').style.display = 'none';
                document.getElementById('Button2').style.display = 'none';
                document.getElementById('Button3').style.display = 'block';
                textBox1.focus();

        };
    </script>
           <script type="text/javascript">
               window.onload = function () {
                   var label8 = document.getElementById('Label8');
                   var label9 = document.getElementById('Label9');
                   var label11 = document.getElementById('Label11');

                   var textBox10 = document.getElementById('<%= TextBox10.ClientID %>');
                   var textBox1 = document.getElementById('<%= TextBox1.ClientID %>');

                   var label8Content = label8 ? label8.textContent.trim() : '';
                   var label9Content = label9 ? label9.textContent.trim() : '';
                   var label11Content = label11 ? label11.textContent.trim() : '';
                   textBox10.focus();

                   if (label8Content === '1') {
                       // มีข้อมูลใน Label7
                       document.getElementById('Barcode').style.display = 'block';
                       document.getElementById('Camerabarcode').style.display = 'block';
                       document.getElementById('TextBox1').style.display = 'block';
                       document.getElementById('GridCustomer').style.display = 'block';
                       document.getElementById('list').style.display = 'block';
                       document.getElementById('Gridin').style.display = 'none';
                       document.getElementById('listin').style.display = 'none';
                       document.getElementById('Button2').style.display = 'none';
                       document.getElementById('Button3').style.display = 'block';
                   }
                   if (label9Content === 'True' && label11Content!=='True' ) {
                       // มีข้อมูลใน Label7
                       document.getElementById('Barcode').style.display = 'block';
                       document.getElementById('Camerabarcode').style.display = 'block';
                       document.getElementById('TextBox1').style.display = 'block';
                       document.getElementById('GridCustomer').style.display = 'block';
                       document.getElementById('GridSave').style.display = 'none';
                       document.getElementById('list').style.display = 'block';
                       document.getElementById('Gridin').style.display = 'none';
                       document.getElementById('listin').style.display = 'none';
                       document.getElementById('Button2').style.display = 'none';
                       document.getElementById('Button3').style.display = 'block';
                       textBox1.focus();

                   }
                   if (label11Content === 'True') {
                       // มีข้อมูลใน Label7
                       document.getElementById('Barcode').style.display = 'none';
                       document.getElementById('Camerabarcode').style.display = 'none';
                       document.getElementById('TextBox1').style.display = 'none';
                       document.getElementById('GridCustomer').style.display = 'none';
                       document.getElementById('list').style.display = 'none';
                       document.getElementById('Gridin').style.display = 'none';
                       document.getElementById('listin').style.display = 'none';
                       document.getElementById('Button2').style.display = 'none';
                       document.getElementById('Button3').style.display = 'none';
                       textBox1.focus();

                   }
                   if (label9Content === 'False' && label9Content !== null) {
                       document.getElementById('Button2').style.display = 'block';

                   }
                   else{
                       document.getElementById('Button2').style.display = 'none';

                   }

               };
    </script>
<script>
            function confirmTransfer() {
                const quantity = document.getElementById('<%= TextBox13.ClientID %>').value;
                const sumud30 = parseInt(document.getElementById('<%= sumud30.ClientID %>').innerText, 10);


        Swal.fire({
            title: `<span style="color: red; font-size: 30px;">โอนแล้วยกเลิกไม่ได้!`,
            text: `คุณยืนยันจะโอน\nเสาจำนวน ${quantity} ต้น \n\nจากที่ต้องการส่ง${sumud30} ต้น `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'ใช่, ดำเนินการต่อ',
            cancelButtonText: 'ยกเลิก'
        }).then((result) => {
            if (result.isConfirmed) {
                // แสดง Loading Screen
                Swal.fire({
                    title: 'กำลังดำเนินการ...',
                    text: 'กรุณารอสักครู่',
                    icon: 'info',
                    allowOutsideClick: false,
                    showConfirmButton: false,
                    willOpen: () => {
                        Swal.showLoading();
                    }
                });

                // เรียก PostBack เพื่อเรียก Button2_Click1
                __doPostBack('<%= Button2.UniqueID %>', '');
            }
        });

            // ป้องกันการส่งข้อมูลอัตโนมัติ
            return false;
        }
</script>

<script>
            function confirmCancel() {
                const textBoxValue = document.getElementById('<%= TextBox10.ClientID %>').value.trim();  // ดึงค่า TextBox10 และลบช่องว่างทั้งก่อนและหลัง

        // เช็คว่า TextBox10 ว่างหรือไม่
        if (textBoxValue === "") {
            Swal.fire({
                title: 'ข้อมูลว่าง',
                text: 'กรุณากรอกข้อมูลใน TextBox10 ก่อน!',
                icon: 'warning',
                confirmButtonText: 'ตกลง'
            });
            return false;  // ถ้าว่างให้หยุดการทำงาน
        }

        // ถ้า TextBox10 มีข้อมูล ให้แสดง SweetAlert แจ้งเตือน
        Swal.fire({
            title: `<span style="color: red; font-size: 30px;">แน่ใจหรือไม่!</span>`,
            text: `คุณต้องการยกเลิก\nการยิงใบ ${textBoxValue} \nใช่หรือไม่?`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'ใช่, ยกเลิก',
            cancelButtonText: 'ไม่, กลับไป',
        }).then((result) => {
            if (result.isConfirmed) {
                // เรียก PostBack สำหรับ Button1
                __doPostBack('<%= Button1.UniqueID %>', '');
            }
        });

            // ป้องกันการ PostBack อัตโนมัติ
            return false;
        }
</script>

</body>
</html>
