<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SectionAlotment.aspx.vb" Inherits="Examinationjune_SectionAlotment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
  <script src="../LeadJquery/jquery1.9.1.min.js" type="text/javascript"></script>
  <style>
      body
        {
            background-color: #f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }
     
         body::-webkit-scrollbar {
            display: none;
        }
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
    .viewprofile
    {
   color:gray;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  .viewprofile:hover {
   color:black;
   font-weight: 600;
    } 
     .communicatesub
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    }
 .communicatesub:hover
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid #1ed085;
    }
  .btncanelg
  {
      margin-left:270px;
      }
    .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}

 .backbotton
{
    font-size:22px;
    font-weight:600;
    color:#7c858f; 
}

.backbotton:hover
{
    color:#15283c;
}  
 .backbotton1
{
    font-size:22px;
    font-weight:600;
    color:#fff; 
}

.backbotton1:hover
{
    color:#fff;
}  

 .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:500;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
 .DownloadExcel
 {
   color:gray;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  .DownloadExcel:hover {
   color:black;
   font-weight: 600;
    } 
#Panel2
{
    min-height:400px;
    max-height:400px;
    }  

.Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      height: 40px;
      cursor: pointer;
      color:White;
      background-color:#1ed085;
      border:none;
      width:24%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
     .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:8px;
       margin-bottom:8px;
       
   }
 .card-header
 {
     background-color:#152837;
     color:#fff;
     font-weight:500;
     font-size:22px;
     letter-spacing:1px;
     }
.addsubject
    {
   color:#15283c;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:1px solid #15283c; 
   background-color:#fff;
   text-decoration:none;
   text-align :center;
   border-radius:4px;
   padding:4px 10px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    } 
  .lblHostel
 {
     font-size:19px;
     }
  .lblHostel1
 {
     font-size:19px;
     }
      .lablname
    {
        padding-left:40px;
    }
 .lablname  .Labels
       {
           
            font-size:17px;
    font-weight:500;
    color:#15283c;
           }
 .hiddencol
 {
     display:none;
 }
 </style>
</head>
<body>
    <form id="form1" runat="server">
    

    <asp:Panel ID="pnlhead" Visible="True"   runat="server">
     <div class="container-fluid">
 
       <div class="row justify-content-center mt-1">
        <div class="col-md-12">
        <div class="card">
        <div class="card-header">
        <div class="d-flex">
        &nbsp
         <asp:LinkButton ID="backbotton" class="backbotton1" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        &nbsp
            <h5 class="program">
                <asp:Label ID="lbl" runat="server" Text="Section Allotment"></asp:Label></h5>
        
          </div>  
       </div>
       
       <div class="card-body">
         
         <div class="row">
      <div class="col-md-6">
     <div class="lablname"> 
              <asp:Label ID="Labl" runat="server" class="Labels" Text="Selected student:"> </asp:Label>&nbsp
              <asp:Label ID="Lblstucount" runat="server" class="Labels" Text="N/A"></asp:Label>
            
              </div>
      </div>
      <div class="col-md-6 text-end">
       
      </div>
     </div>

         <div class="row mt-4">
          <div class="col-md-1">
          </div>
          <div class="col-md-3">
            <label >Section : </label>
          </div>
           <div class="col-md-6">
               <asp:DropDownList ID="Ddlsec" class="form-select" runat="server">
               </asp:DropDownList>
          </div>
           <div class="col-md-2">
          </div>
         </div>

         <div class="row mt-2">
          <div class="col-md-1">
          </div>
          <div class="col-md-3">
            <label >Class Room : </label>
          </div>
           <div class="col-md-6">
               <asp:DropDownList ID="Ddlclassroom" class="form-select" runat="server">
               </asp:DropDownList>
          </div>
           <div class="col-md-2">
          </div>
         </div>

         <div class="row mt-2">
         <div class="col-md-12 text-center">
            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn Submit" Width="14%"/>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn Submit" Visible="false" Width="14%"/>
          </div>
          </div>

       </div>

      </div>
      </div>
      </div>
  
    </div>
    </asp:Panel>
    </form>
</body>
</html>
