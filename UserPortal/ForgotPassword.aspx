<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb" Inherits="UserPortal_ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.4.1/css/simple-line-icons.min.css" rel="stylesheet">
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />  
    <style>
    body{
    background-color: #f1f2f3;
}

.registration-form{
	padding: 10px 0;
	margin-top:20px;
}

.registration-form .form{
    background-color: #fff;
    max-width: 450px;
    margin: auto;
    padding: 20px 40px;
    border-top-left-radius: 30px;
    border-top-right-radius: 30px;
    box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.075);
    border-top:1px solid #1ed085;
    border-left:1px solid #1ed085;
    border-right:1px solid #1ed085;
}

.registration-form .form-icon{
	text-align: center;
    background-color: #1ed085;
    border-radius: 50%;
    font-size: 40px;
    color: white;
    width: 100px;
    height: 100px;
    margin: auto;
    margin-bottom: 25px;
    padding-top:25px;
}

.registration-form .item{
	border-radius: 20px;
    margin-bottom: 25px;
    padding: 10px 20px;
}
.registration-form .item1{
	border-radius: 10px;
    padding: 10px 20px;
}

.registration-form .create-account{
    border-radius: 30px;
    padding: 8px 18px;
    font-size: 18px;
    font-weight: bold;
    background-color: #1ed085;
    border: none;
    color: white;
    margin-top:-9px;
}
.registration-form .create-account1{
    border-radius: 30px;
    padding: 8px 18px;
    font-size: 18px;
    font-weight: bold;
    background-color: #1ed085;
    border: none;
    color: white;
    margin-top:16px;
}
.registration-form .social-media{
    max-width: 450px;
    background-color: #fff;
    margin: auto;
    padding: 18px 0;
    text-align: center;
    border-bottom-left-radius: 20px;
    border-bottom-right-radius: 20px;
    color: #1ed085;
    border-top: 1px solid #d9dbd9;
    box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.075);
    border-bottom:1px solid #1ed085;
    border-left:1px solid #1ed085;
    border-right:1px solid #1ed085;
}

.registration-form .social-icons{
    margin-top: 5px;
    margin-bottom: 5px;
}

.registration-form .social-icons a{
    font-size: 23px;
    margin: 0 3px;
    padding-top:10px;
    color: #1ed085;
    border: 1px solid;
    border-radius: 50%;
    width: 45px;
    display: inline-block;
    height: 45px;
    text-align: center;
    background-color: #fff;
    line-height: 45px;
    text-decoration:none;
}

.registration-form .social-icons a:hover{
    text-decoration: none;
    opacity: 0.9;
    background:#1ed085;
    color:#fff;
}
.textdownshortcut
{
    margin-bottom:-8px;
    margin-top:10px;
}
.textdownshortcut
{
    margin-bottom:-8px;
    margin-top:10px;
}
@media (max-width: 576px) {
    .registration-form .form{
        padding: 50px 20px;
    }

    .registration-form .form-icon
    {
        width: 70px;
        height: 70px;
        font-size: 30px;
        line-height: 70px;
        margin-bottom: 25px;
        padding:18px;
    }
  
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
 <div class="registration-form p-2 mt-5">
        <div class="form">
            <div class="form-icon">
                <span><i class="icon icon-user"></i></span>
            </div>
            <asp:Panel ID="PnlForgetPassword" runat="server" Visible="true">
            <div class="form-group text-center">
              Enter your email address below. We'll email you a link to a page where you can easily create a new password.              
            </div>
            <div class="form-group mt-3">
              <asp:TextBox ID="txtEmailPhone" runat="server" CssClass="form-control item" placeholder="Email ID/Mobile No"></asp:TextBox>               
            </div>
                     
            <div class="form-group">
                <asp:LinkButton ID="btnResetPwd" runat="server" type="button" class="btn btn-block create-account" Width="100%">Reset Password</asp:LinkButton>
            </div>
            <div class="form-group text-center textdownshortcut" >    
            Don't have account ?        
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="text-decoration-none" ForeColor="#1ed085" PostBackUrl="~/LoginFinal.aspx">
                 Login
            </asp:LinkButton>               
            </div>
            </asp:Panel>
            <asp:Panel ID="PnlOTP" runat="server" Visible="false">
             <div class="form-group text-center">
               &nbsp;&nbsp; Submit Your sent on +91 <asp:Label ID="lblPhoneNo" runat="server">*******154</asp:Label> OTP and enjoy your dashboard section            
            </div>
             <div class="form-group mt-4">
               <table width="100%">
                <tr width="100%">
                <td width="5%"></td>
                <td width="20%">
                 <asp:TextBox ID="txtOtp1" runat="server" CssClass="form-control item1"></asp:TextBox>
                </td>
                <td width="3%"></td>
                <td width="20%">
                 <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control item1"></asp:TextBox>
                </td>
                <td width="3%"></td>
                <td width="20%">
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control item1"></asp:TextBox>
                </td>
                <td width="3%"></td>
                <td width="20%">
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control item1"></asp:TextBox>
                </td>
                <td width="5%"></td>
                </tr>
               </table>
             </div>
            
             <div class="form-group">
                <asp:LinkButton ID="btnSubmit" runat="server" type="button" class="btn btn-block create-account1" Width="100%">Submit</asp:LinkButton>
            </div>

            <div class="form-group text-center textdownshortcut" >    
            Don't have account ?        
            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#1ed085" CssClass="text-decoration-none" PostBackUrl="~/LoginFinal.aspx">
                 Login
            </asp:LinkButton>               
            </div>

           </asp:Panel>
        </div>
        <div class="social-media">
            <div class="social-icons">
                <a href="#"><i class="icon-social-facebook" title="Facebook"></i></a>
                <a href="#"><i class="icon-social-google" title="Google"></i></a>
                <a href="#"><i class="icon-social-twitter" title="Twitter"></i></a>
            </div>
        </div>
    </div>
   
    </form>
     <script type="text/javascript" src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.15/jquery.mask.min.js"></script>
    
    <script>
        $(document).ready(function () {
            $('#birth-date').mask('00/00/0000');
            $('#phone-number').mask('0000-0000');
        })
    </script>
</body>
</html>
