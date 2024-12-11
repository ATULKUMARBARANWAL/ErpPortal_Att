<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UnitDocuments.aspx.vb" Inherits="Examinationjune_UnitDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <title></title>
    <style>
    
    body 
{
    background-color:#f2f3f5;
}
 .heading1
{
    padding-top:2px;
    }
  .heading1 h3
{
    color:#15283c;
    font-size:20px;
    font-weight:600;
   
    }
    
     .line
   {
       content:" ";
       width:100%;
       height:1px;
        background-color:#e1e2e3;
        margin-top:2px;
        margin-bottom:5px;
        
       
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

  
       .Labels
       {
           
            font-size:17px;
    font-weight:500;
    color:#000;
           }
    
    .row1
    {
        margin-top:20px;
        }
        
        #lbtnDoc
        {
            color:White;
            background:#1ed085;
            border:3px solid #f0f0f0;
            }
        #lbtnvideo
        {
            border:3px solid #f0f0f0; }
            
           .button_outer {background: #1ed085; border-radius:30px; 
               text-align: center; height: 50px; 
               width: 50px; display: inline-block; 
              position: relative; overflow: hidden;}
 .button_outer:hover {background: #1aad6f; 
                      box-shadow:0px 1px 5px 1px #e1e2e3;
                      }

.btn_upload {padding: 15px 12px;
              color: #fff; 
             text-align: center; position: relative; font-size:18px;  font-weight:bold;
             display: inline-block; overflow: hidden; z-index: 3; white-space: nowrap;
            }
.btn_upload #upload_file {position: absolute; width: 100%; left: 0; top: 0;
                           width: 100%; height: 105%; cursor: pointer; opacity: 0;}


.floatbtn
{
    right:40px;
    top:90px;
      position: fixed;
      
    }
    
    .floatbtn.sticky{
     right:10px;
    
     
}


    .upload
    {
        background: #1ed085;
        color:#fff;
        }
        
         .upload:hover {background: #1aad6f; 
                      box-shadow:0px 1px 5px 1px #e1e2e3;
                       color:#fff;
                      }
                      
                      
                      
           #lbtnvideo
           {
               
               color:Black;
               }
               
               .doccontainer{
    
    width:100%;
    position: relative;
    
    }
    
    .doccontainer img {
  width: 40%;
  height: auto;
}

.doccontainer .downloaddoc {
  position: absolute;
  top: 15%;
  left: 75%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.doccontainer .downloaddoc:hover {
 
  background-color: #F0f0f0;
  color: black;

}

.doccontainer .deletedoc {
  position: absolute;
  top: 40%;
  left: 75%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: Gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.doccontainer .deletedoc:hover {
 
  background-color: #F0f0f0;
  color: black;
 
}

  .vidcontainer{
    
    width:100%;
    position: relative;
    
    }
 
   .vidcontainer .video
   {
        width:130px ;
  height: 100px;
   }

.vidcontainer .downloaddoc {
  position: absolute;
  top: 20%;
  left: 95%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.vidcontainer .downloaddoc:hover {
 
  background-color: #F0f0f0;
  color: black;

}

.vidcontainer .deletedoc {
  position: absolute;
  top: 39%;
  left: 95%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: Gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.vidcontainer .deletedoc:hover {
 
  background-color: #F0f0f0;
  color: black;
 
}
 .maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
}            
    </style>

    
    
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid maincontainer">

     
    <div class="row">
   
   
 
   
            <div class="col-md-5 mt-1">
          
           <table>
    <tr width="100%">
    <td width="30%">
        <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    </td>
     <td  width="70%">
        <div class="heading1">
   <h3>Unit Item </h3>
   </div>
     </td>
    </tr>
    </table>
             
              
            </div>
            
            <div class="col-md-4 mt-1">
          
        
     
            </div>


            <div class="col-md-3 mt-1">
            <table>
            <tr width="100%">

           

            <td width="50%">
             <asp:Label ID="Label1" runat="server" class="Labels" Text="Academic Year :"></asp:Label>
             </td>
            <td width="50%">
             <asp:Label ID="lblAcademicyear" class="Labels" runat="server" Text=" Academic Year "></asp:Label>
            </td>
            
            </tr>
            </table>
                
            </div>
             <div class="line">
         </div>
           
             
             
            </div>

    <div class="row">
        
           
          
            <div class="col-md-4">
            <table>
            <tr width="100%">
            
          
              <td width="80%">
               <asp:Label ID="Label2" runat="server" class="Labels" Text="Subject :"></asp:Label>
                <asp:Label ID="lblsubject" runat="server" class="Labels" Text=" Program "></asp:Label>
              </td>
              <td width="100%"></td>
                 
                
            </tr>
            </table>
            
            
            </div>
             <div class="col-md-7 text-end">
              <asp:Label ID="Label3" runat="server" class="Labels" Text="Unit Name :"></asp:Label>
                <asp:Label ID="lblunitname"  runat="server" class="Labels" Text=" Unit "></asp:Label>
                </div>
                <div class="col-md-1"></div>
            </div>
            
      <div class="row row1">
                       <div class="col-md-12">
                               <nav>
                                   <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                    <asp:LinkButton class="nav-item nav-link active" ID="lbtnDoc" runat="server">Documents</asp:LinkButton>
                                    <asp:LinkButton class="nav-item nav-link " ID="lbtnvideo" runat="server">Videos</asp:LinkButton>
                                     </div>
                                </nav>
                    
                      </div>
                       </div>

         <asp:Panel ID="paneldoc" runat="server">
        
           <div class="row">

           <asp:Repeater ID="Repeater2" runat="server">
              <ItemTemplate>

           <div class="col-md-2 text-center mt-2">
              <div class="doccontainer">
              <img src="../ExaminationNImages/j4cuv0iciu1adu5oef0imtkca8-92728334e12d3f4c8c60ace58f4ba84d.png" />
               <span><asp:LinkButton ID="lbtndownloaddoc" runat="server" class="downloaddoc" CommandArgument='<%# Eval("Path") %>' OnClick="DownloadFile"><i class="fa fa-download"  ></i></asp:LinkButton></span>
             
               <asp:LinkButton ID="lbtndeletedoc" CommandArgument='<%# Eval("Path") %>'  class="deletedoc" OnClick="DeleteFile" OnClientClick="return confirm('Really!Do u want to Delete')" runat="server"><i class="fa fa-trash"></i></asp:LinkButton>
             <h2><asp:Label ID="Lbltopicdoc" class="Labels" runat="server" Text='<%# Eval("Topic") %>'></asp:Label></h2>
              </div>
           </div>

           </ItemTemplate>
           </asp:Repeater>
           
           </div>

            </asp:Panel>


             <asp:Panel ID="panelvideo" Visible="false" runat="server">
        
           <div class="row">

            <asp:Repeater ID="Repeater1" runat="server">
              <ItemTemplate>

           <div class="col-md-2 text-center mt-1">
             <div class="vidcontainer">
                <video  class="video" controls>  
                                <source src='<%#Eval("Path")%>' type="video/mp4">  
                            </video>  
                               <span> <asp:LinkButton ID="lbtndownloadvid" runat="server" class="downloaddoc" CommandArgument='<%# Eval("Path") %>' OnClick="DownloadFile"><i class="fa fa-download"></i></asp:LinkButton></span>
            <asp:LinkButton ID="lbtndeletevid" runat="server"  CommandArgument='<%# Eval("Path") %>' class="deletedoc" OnClientClick="return confirm('Really!Do u want to Delete')" OnClick="DeleteFile"><i class="fa fa-trash"></i></asp:LinkButton>

             <h2><asp:Label ID="lbltopicvid" class="Labels" runat="server"  Text='<%# Eval("Topic") %>'></asp:Label></h2>
             
              </div>
              
               

           </div>

           </ItemTemplate>
           </asp:Repeater>
           
           </div>

            </asp:Panel>

        <div class="floatbtn" >
  	<div class="button_outer">
				<div class="btn_upload">
				
                    <asp:Button ID="upload_file" runat="server" Text=""  data-bs-toggle="modal" data-bs-target="#exampleModal" OnClientClick="return false;" />
					<asp:Label ID="lblselectfile" runat="server" CssClass="pr-5" Text=""><i class="fa-solid fa-plus"></i></asp:Label>
                    
				</div>
			</div>
            </div>


     


<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">
            <asp:Label ID="lblheadingpopup" runat="server" Text="Upload Documents"></asp:Label>
        </h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10">
        <table>
        <tr width="100%">
        <td width="30%"> <asp:Label ID="Label5" runat="server" Text="Topic :"></asp:Label></td>
          <td width="70%">  <asp:TextBox ID="txttopic" Width="280px" runat="server" class="form-control"></asp:TextBox></td>
        </tr>
        </table>
           
          
        </div>
       <div class="col-md-1"></div>
        </div>

          <div class="row row1">
       <div class="col-md-1"></div>
        <div class="col-md-11">
        <table>
        <tr width="100%">
        <td width="70%"> <asp:Label ID="lbldocname" runat="server" Text="Documents :"></asp:Label></td>
          <td width="30%"> 
              <asp:FileUpload ID="uploadfile" runat="server" /></td>
        </tr>
        </table>
           
          
        </div>
      
        </div>
      </div>
      <div class="modal-footer">
        
       <%-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>--%>
        
          <asp:Button ID="btnupload" runat="server" class="btn upload" Text="Upload" />
        
      </div>
    </div>
  </div>
</div>

             </div>
    </form>
    

    
</body>
</html>
