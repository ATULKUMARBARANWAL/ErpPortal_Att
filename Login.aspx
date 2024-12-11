<%@ page language="VB" autoeventwireup="false" inherits="Login, App_Web_xeolm3hz" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>   
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Bootstrap5/Bootstrap-4.3.1-new/bootstrap.min.css"  rel="stylesheet" type="text/css" />
    <link href="AR/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <link href="Styles/deepakstyle.css" rel="stylesheet" type="tesdxt/css" />
    <style type="text/css">
        .vl
        {
            border-left: 2px solid #0093dd;
            height: 114px;
        }

        .v2
        {
            border-left: 2px solid #0093dd;
            height: 80px;
        }
        
        .alert-light
        {
            border-color: #c4c4ce;
            padding: 0px;
        }
    </style>
    <style type="text/css">
        .hide
        {
            display: none;
        }
        
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            position: relative;
        }
        .toprow
{
    padding:10px 24px;
    background:#152837; 
        background-image: url('ImageS/pattern_h.png');
    color:#faf7f2;
}
.toprow .logo
{
    width:160px;
}
.toprow .logopop
{
    width:100%;
}

.loginborder
{
    border:2px solid #1ed085;
    border-radius:18px;
    }
.btnLogin
{
     background-color:#1ed085;
     font-size: 18px !important;
      font-weight: 500;
     color:#fff;
     width:10rem;
    }
.btnLogin:hover
{
    background-color:#1ed085;
    border:1px solid #1ed085;
    color:#fff;
  }
  .DownloadShort
  {
      color:#152837;
      font-weight:500;
      }
  .DownloadShort:hover
  {
      color:#152837;
      font-weight:500;
      text-decoration:underline;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid deepakcontainer">
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <div>
        <div class="row toprow">
                               <div class="col-md-3">
                                   <asp:Image ID="Image5" CssClass="logo" runat="server" ImageUrl="img/sarallogo.png"  />
                               </div>
                               <div class="col-md-6 d-flex justify-content-center ">
                               <h3>
                                   SARAL </h3>
                               </div>
                               <div class="col-md-3"></div>
                            </div>
            
        </div>
        <div class="row mt-5">
            <div class=" col-sm-4 col-md-4 col-lg-4 "></div>
            <div class=" col-sm-4 col-md-4 col-lg-4 ">
                <div class="alert loginborder">
                    <div class="row">
                        <div class="mx-auto pb-4">
                            <asp:Image ID="Image1"  ImageUrl="img/loginimg.jpg" Width="150px" Height="150px" runat="server" />
                        </div>
                    </div> 
                    <div class=" col mt-4">
                        <div class="form-group col-sm-12 col-md-12 col-lg-12 ">
                            <label>
                                User Name
                            </label>
                            <div class="input-group ">
                                <div class="input-group-prepend">
                                    <i class="input-group-text fa fa-user-secret "></i>
                                </div>
                                <input type="text" name="uname" placeholder="Enter User Name"  class="form-control" />
                            </div>
                        </div>
                        <div class="form-group col-sm-12 col-md-12 col-lg-12 mt-1">
                            <label>
                                Password</label>
                            <div class="input-group ">
                                <div class="input-group-prepend">
                                    <i class="input-group-text fa fa-lock "></i>
                                </div>
                                <input type="password" name="psw" placeholder="**************" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group col-sm-12 col-md-12 col-lg-12 mt-2 ml-2">
                            <asp:CheckBox ID="CheckBox1" Text=" Remember Me" runat="server" />
                        </div>
                    </div>
                    <div class="row mt-4 mb-4">
                        <div class="mx-auto">
                            <button id="btnlogin" class="btn btnLogin" onserverclick="btnlogin_Click"
                                runat="server" type="submit">
                                Login</button>
                           
                        </div>
                    </div>
                    <div class="row mt-2 ">
                        <div class="mx-auto">
                            
                            <label   id="lblmsg" runat="server" style="color: #FF0000">
                            </label>
                        </div>
                    </div>
                   <div class="row mt-1 mb-2 ">
                        <div class="mx-auto">
                       
                             <a id="A11" href="" class="DownloadShort" runat="server" >Download App Shortcut</a>   
                        </div>
                    </div>
                </div>
            </div>
            <div class=" col-sm-4 col-md-4 col-lg-4 "></div>
        </div>
        <div style="background-color: #66CCFF">
            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="alert alert-light p-4" >
                <div class="hero-image">
                    <div class="row toprow">
                               <div class="col-md-3">
                                   <asp:Image ID="Image2" CssClass="logopop" runat="server" ImageUrl="img/sarallogo.png"  />
                               </div>
                               <div class="col-md-9 d-flex justify-content-center ">
                               <h5>
                                   SARAL </h5>
                               </div>
                            </div>
                    <hr />
                    <div class="col-sm-9 col-md-9 col-lg-9 ">
                        Choose any Roles & Academic Session
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="rblr" class="hero-text">
                                <h1 style="font-size: 25px; color: rgb(0,0,255)">
                                    <asp:RadioButtonList ID="rblroles" Visible="false" CssClass="font-weight-bold p-2" AutoPostBack="true"
                                        runat="server">
                                    </asp:RadioButtonList>
                                </h1>
                            </div>
                            <div class="hero-text">
                                <h1 style="font-size: 25px; color: rgb(0,0,255)">
                                    <asp:RadioButtonList ID="rblsession" CssClass="font-weight-bold p-2" AutoPostBack="true"
                                        runat="server">
                                    </asp:RadioButtonList>
                                </h1>
                            </div>
                        </ContentTemplate>
                        <%-- <Triggers >
         <asp:AsyncPostBackTrigger ControlID="rblroles" EventName="SelectedIndexChanged" />
       <asp:AsyncPostBackTrigger ControlID="rblsession" EventName="SelectedIndexChanged" />
   </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
                <asp:Button ID="btnShow" CssClass="btn btn-primary hide" runat="server" Text="Show Modal Popup" />
                <asp:Button ID="btnClose" CssClass="btn btn-primary hide" runat="server" Text="Close" />
                <asp:Button ID="Button2" CssClass="btn btn-primary hide" runat="server" Text="Login as" />
            </asp:Panel>
        </div>
    </div>
    </form>
    <%--deepak lib start--%>
    <script src="../Bootstrap-4.3.1-new/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Bootstrap-4.3.1-new/jquery.min.js" type="text/javascript"></script>
    <script src="../Bootstrap-4.3.1-new/popper.min.js" type="text/javascript"></script>
    <%--deepak lib end--%>
</body>
</html>
