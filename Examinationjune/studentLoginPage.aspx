<%@ Page Language="VB" AutoEventWireup="false" CodeFile="studentLoginPage.aspx.vb" Inherits="TESTFILES_LoginPage" %>
<%--
'Design By Shivani And Developed By Avaneesh Yadav--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login page in HTML</title>
    <style>
        body 
{
  font-family:sans-serif; 
  background: -webkit-linear-gradient(to right, #155799, #159957);  
  background: linear-gradient(to right, #155799, #159957); 
  color:whitesmoke;
}


h1{
    text-align: center;
}


form{
    width:25rem;
    margin: auto;
    color:whitesmoke;
    backdrop-filter: blur(16px) saturate(180%);
    -webkit-backdrop-filter: blur(16px) saturate(180%);
    background-color: rgba(11, 15, 13, 0.582);
    border-radius: 12px;
    border: 1px solid rgba(255, 255, 255, 0.125);
    padding: 20px 25px;
    margin-top:40px;
}

input[type=text], input[type=password]{
    width: 100%;
    margin: 10px 0;
    border-radius: 5px;
    padding: 15px 18px;
    box-sizing: border-box;
  }

.Button {
    background-color: #030804;
    color: white;
    padding: 14px 20px;
    border-radius: 5px;
    margin: 7px 0;
    width: 100%;
    font-size: 18px;
  }

.Button:hover {
    opacity: 0.6;
    cursor: pointer;
}

.headingsContainer{
    text-align: center;
}

.headingsContainer p{
    color: gray;
}
.mainContainer{
    padding: 16px;
}


.subcontainer{
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
}

.subcontainer a{
    font-size: 16px;
    margin-bottom: 12px;
}

span.forgotpsd a {
    float: right;
    color: whitesmoke;
    padding-top: 16px;
  }

.forgotpsd a{
    color: rgb(74, 146, 235);
  }
  
.forgotpsd a:link{
    text-decoration: none;
  }

  .register{
    color: white;
    text-align: center;
  }
  
  .register a{
    color: rgb(74, 146, 235);
  }
  
  .register a:link{
    text-decoration: none;
  }

  /* Media queries for the responsiveness of the page */
  @media screen and (max-width: 600px) {
    form{
      width: 25rem;
    }
  }
  
  @media screen and (max-width: 400px) {
    form{
      width: 20rem;
    }
  }
    </style>
</head>
<body>
    <form id="form1" runat="server">
          <h1>Login</h1>
    <form action="">
     
        <div class="mainContainer">
           
            <label for="username">Your username</label>
            <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Username" type="text"></asp:TextBox>
            <br><br>

            <label for="pswrd">Your password</label>
            <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Password" type="password"></asp:TextBox>
            <div class="subcontainer">
                <label>
                  <input type="checkbox" checked="checked" name="remember"> Remember me
                </label>
                <p class="forgotpsd"> <a href="#">Forgot Password?</a></p>
            </div>
            <asp:Button ID="Button1" runat="server"  CssClass="Button" Text="Login" />
            
            <p class="register">Not a member?  <a href="#">Register here!</a></p>

        </div></form>
    </form>
</body>
</html>
