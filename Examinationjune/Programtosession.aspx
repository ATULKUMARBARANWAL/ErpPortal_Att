<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Programtosession.aspx.vb" Inherits="Examinationjune_Programtosession" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />

<style>

    .messagealert {
            width: 50%;
            position: fixed;
            left:200px;
             top:0%;
            z-index: 100;
            padding: 0;
            font-size: 15px;
        }

 .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      width:20%;
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }




     
      #Panel2
 {
     margin-top:50px;
     }

          .maincontainerm
{
 background-color:#f2f3f5;
    color:#15283c; 
    font-size:16px; 
    width:40px;
    height:40px ;
    border-radius:50%;
    padding-top:11px;
    
    margin-left:50px;
  }
#btnlefttransfer
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
   
}
#btnlefttransfer:hover
{
    color:#15283c;
}

    .maincontainerr
{
    background-color:#f2f3f5;
    color:#15283c; 
    font-size:16px; 
    width:40px;
    height:40px ;
    border-radius:50%;
    padding-top:11px;
    margin-top:70px;
    margin-left:50px;
  }
#btnRighttransfer
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
   
}
#btnRighttransfer:hover
{
    color:#15283c;
}

#backbotton
{
     font-size:22px;
    font-weight:600;
    color:#7c858f;
   
    }

    #backbotton:hover
{
     color:#15283c;
    }

.hiddencol
{
    display:none;
    }



    body 
{
    background-color:#f2f3f5;
}
.maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
}

.maincontainerback
{
     
    color:#15283c; 
    font-size:16px; 
    width:40px;
    height:40px ;
    border-radius:50%;
    padding-top:11px;
    margin-top:70px;
    margin-left:50px;
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
    
    .subheading
{
    padding-top:7px;
    }
  .subheading h3
{
    color:#15283c;
    font-size:24px;
    font-weight:400;
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
   
   #btncopy
   {
       color:#7c858f;
       }

     #btncopy:hover
   {
       color:#15283c;
       }

    .row1
    {
        margin-top:-30px;
        }
        
        .lblacdemic
        {
            
            font-weight:700;
            
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
   padding:3px 10px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    }
    
    
    #Panel1
    {
        
        min-height:30px;
        max-height:470px;
        
        }
        
          .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
</style>




</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="container-fluid maincontainer">

        

            <asp:Panel ID="Panelmap" runat="server">
            
            <div class="row">
            <div class="col-md-5 mt-1">
          
             
                   <div class="heading1">
   <h3>Add Program to Academic Year(
       <asp:Label ID="lblacdemicyear" runat="server" Text=""></asp:Label>&nbsp;)</h3>
   </div>
            </div>
            
            <div class="col-md-3 mt-1">
          
        
     
            </div>


            <div class="col-md-4 mt-1">
            <table>
            <tr width="100%">
            <td width="35%">
             <asp:Label ID="Label1" runat="server" class="lblacdemic" Text="Academic Year :"></asp:Label>
             </td>
            <td width="50%">
             <asp:DropDownList ID="ddlacademicyear" autopostback=true class="form-select" runat="server">
                 </asp:DropDownList>
            </td>
            <td width="5%"></td>
            <td width="10%">
             <asp:LinkButton ID="btncopy" runat="server" ToolTip="Copy Subject From Previous Year"><i class="fa fa-copy" style="font-size:20px;"></i></asp:LinkButton>
            </td>
            </tr>
            </table>
                
            </div>
             <div class="line">
         </div>
           
             
             
            </div>

                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical">
                
        
            
            <asp:GridView ID="GridCoursemapsession" Width="100%"  ShowHeaderWhenEmpty="True" OnRowEditing="OnRowEditing"
            AutoGenerateColumns="False" runat="server"  Class="table table-bordered" DataKeyNames="Coursesessionid" >
             <Columns>
               
               <asp:TemplateField HeaderText="S.No">

                <ItemTemplate>
                  <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>

               </asp:TemplateField>
                                      
                   <asp:BoundField HeaderText="Courseid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  DataField="Courseid" ReadOnly="true"></asp:BoundField>                                 
                  <asp:BoundField HeaderText="Program"  DataField="Coursecode" ReadOnly="true"></asp:BoundField> 
                 <%-- <asp:BoundField HeaderText="Course Code"  DataField="Coursecode" ReadOnly="true"></asp:BoundField> --%>
                 

                   <asp:TemplateField HeaderText="Program Level">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlCourselevel" runat="server">
                    <asp:ListItem>UG</asp:ListItem>
                    <asp:ListItem>PG</asp:ListItem>
                    <asp:ListItem>Diploma</asp:ListItem>
                    </asp:DropDownList>
              </ItemTemplate>
              </asp:TemplateField>

                       <asp:TemplateField HeaderText="Program Type">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlCoursetype" runat="server">
                    <asp:ListItem value="Sem">Semester</asp:ListItem>
                    <asp:ListItem value="Year">Year</asp:ListItem>
                    </asp:DropDownList>
              </ItemTemplate>
              </asp:TemplateField>             
                  
                    <asp:TemplateField HeaderText="Duration(in years)" >
                <ItemTemplate>
                <asp:TextBox ID="txtduration" runat="server"  />
                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"  Enabled="True" TargetControlID="txtduration" FilterType="Numbers" FilterMode="ValidChars">
</cc1:FilteredTextBoxExtender>--%>
<br />
<asp:RangeValidator runat="server" id="valrNumberOfPreviousOwners" ControlToValidate="txtduration" Type="Integer" MinimumValue="0"  MaximumValue="10" ForeColor="red" CssClass="input-error" ErrorMessage="*Invalid Duration"  Font-Size="12px" Display="Dynamic"></asp:RangeValidator>
              </ItemTemplate>
              </asp:TemplateField>
               <asp:TemplateField HeaderText="No. of Seat">
                <ItemTemplate>
                <asp:TextBox ID="txtSeat" runat="server"  />
              </ItemTemplate>
              </asp:TemplateField>
                                                                                                                                  
                     
                      <asp:TemplateField HeaderText="Update">
            <ItemTemplate >
             <asp:LinkButton  Text='Update' ID="namelinkes" runat="server" CommandName="select"   CssClass="CourseName" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  ></asp:LinkButton>       
            </ItemTemplate>
        </asp:TemplateField>
                     
                                                                    
          </Columns>
        </asp:GridView>
   
                                        
             
              </asp:Panel>
               
                 <div class="row mt-3">
                    <div class="col-md-6 text-end">
                    
                        <asp:Button ID="btnSubmit" class="btn Submit" runat="server" Text="Save" />
                    </div>

                     <div class="col-md-6 text-end">
                                     <asp:LinkButton ID="btnmasterprogram" runat="server" class="addsubject" ><i class="fa-solid fa-plus"></i> Program</asp:LinkButton>
                      </div>

            
            </div>

            </asp:Panel>

            <asp:Panel ID="Pnlcopyexamtype" visible="false" runat="server">
            <div class="row maincontent">
            <div class="col-md-1">
            
                  <div class="text-center maincontainerback m-0">
        <asp:LinkButton ID="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
     </div>
            </div>
            <div class="col-md-7">
             <div class="subheading">
                                    <h3>List program type of <asp:Label ID="Lblyear" runat="server" Text="N/A"></asp:Label> </h3>
                                 </div>

             </div>

            <div class="col-md-4 d-flex justify-content-end">
            <table class="tableform" width="100%">
                                                <tr width="100%">
                                                <td width="50%">
                                                  Academic year:
                                                       
                                                </td>
                                                <td width="50%">
                                                
                                                 <div class="form-group">
                                                     <asp:DropDownList ID="Ddlcopyacademicyear" AutoPostBack="true" class="form-control form-select" runat="server">
                                                     </asp:DropDownList>
                                                     </div>
                                                    </td>
                                                    </tr>
                                             
                                                    </table>
            </div>

             <div class="line">
         </div>

            </div>
            <br />
            <div class="row ">
            
       
           
             <div class="col-md-12">
               <asp:GridView ID="gridcopyexamtype" class="table table-bordered" AutoGenerateColumns="false"  runat="server">
               <Columns>
               <asp:TemplateField HeaderText="S.No.">
                         <HeaderTemplate>
                      <asp:CheckBox ID="Checkhead" runat="server" onclick="checkAll(this);true" checked />
                 </HeaderTemplate>
                                         
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"  onclick="Check_Click(this)" Checked />
                      
                    </ItemTemplate>
                         </asp:TemplateField>
                  <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
                  <asp:BoundField HeaderText="Academic Year" DataField="Academicyear"></asp:BoundField>
                           
                           <asp:BoundField HeaderText="Courseid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  DataField="Courseid"></asp:BoundField>
                           <asp:BoundField HeaderText="Program Type" DataField="Coursetype"></asp:BoundField>
                             <asp:BoundField HeaderText="Program" DataField="Course"></asp:BoundField>
                           <asp:BoundField HeaderText="Duration" DataField="Duration"></asp:BoundField>
                             <asp:BoundField HeaderText="Program Code" DataField="Coursecode"></asp:BoundField>
                           <asp:BoundField HeaderText="Program Level" DataField="courselevel"></asp:BoundField>
                            <asp:BoundField HeaderText="No of Seat" DataField="Noofseat"></asp:BoundField>
               </Columns>
                 </asp:GridView>
             </div>
             
            </div>

           <div class="row">
           <div class="col-md-12 text-center">
            <asp:Button ID="btncopyfromprevious" class="btn Submit" runat="server" Text="Copy" />
           </div>
           </div>

          </asp:Panel>
    </form>
</body>
</html>
