<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ThankYouQuiz.aspx.vb" Inherits="UserPortal_ThankYouQuiz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    .thank-you-container {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 97vh;
    background-color: #f4f6f8;
}

.thank-you-content {
    background-color: #ffffff;
    border-radius: 10px;
    padding: 30px;
    text-align: center;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
    max-width: 500px;
    width: 100%;
}

.thank-you-icon {
    color: #28a745;
    font-size: 60px;
    margin-bottom: 15px;
}

.thank-you-content h2 {
    font-size: 24px;
    color: #333333;
    margin-bottom: 10px;
}

.thank-you-content p {
    color: #666666;
    font-size: 16px;
    line-height: 1.6;
    margin-bottom: 20px;
}

.back-button {
    background-color: #28a745;
    color: #ffffff;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.back-button:hover {
    background-color: #218838;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="thank-you-container" id="thankYouContainer" runat="server">
    <div class="thank-you-content">
        <i class="fas fa-check-circle thank-you-icon"></i>
        <h2>Thank You!</h2>
        <p>Thank you for participating in the Quiz! You can check the results and review them on your homepage.</p>
        <asp:Button id="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="back-button" Text="Go Back"/>
    </div>
</div>
    </form>
</body>
</html>
