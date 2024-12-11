<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreviewQuiz.aspx.vb" Inherits="UserPortal_PreviewQuiz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestions_ItemDataBound">
    <ItemTemplate>
        <div class="question">
    <div style="display:flex; justify-content:space-between;">
        <strong>Question <%# Container.ItemIndex + 1 %>: <%# Eval("QuestionText") %></strong>
        <div style="display:flex; justify-content:space-between;">
            <%--<asp:LinkButton ID="btnEditQuestion" runat="server" CommandArgument='<%# Eval("QuestionID") %>' OnClick="EditQuestion_Click" CssClass="edit-icon">
                <i class="fa fa-pencil" style="color:green;font-size:16px;"></i>
            </asp:LinkButton>
            <asp:LinkButton ID="btnDeleteQuestion" runat="server" CommandArgument='<%# Eval("QuestionID") %>' OnClick="DeleteQuestion_Click">
                <i class="fa fa-trash" style="color:Red;font-size:16px;margin-left:5px;"></i>
            </asp:LinkButton>--%>
        </div>
    </div>

    <%# If(Not String.IsNullOrEmpty(Eval("imageQuestion").ToString()),
                    "<img src='" & ResolveUrl(Eval("imageQuestion").ToString()) & "' alt='Question Image' style='max-width:150px; max-height:150px; margin-top:10px;margin-bottom:10px;border-radius:7px;' />",
                    "")%>
    <br />
    <span style="margin-top:10px;">
       <span style="font-weight:bold;">Type: </span><%# Eval("QuestionType") %><br />
    </span>

<div style="display: flex; gap: 20px; margin-top: 15px;">
    <!-- Correct Feedback Label -->
    <asp:Label ID="getCorrectFeedback" runat="server"
               Text='<%# Eval("FeedbackCorrect") %>'
               Visible='<%# Not String.IsNullOrEmpty(Eval("FeedbackCorrect").ToString()) %>'
               style="background-color: #e6ffe6; color: #2e7d32; font-size: 1.1em; padding: 10px 15px; 
                      border-radius: 8px; box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); 
                      max-width: 45%; text-align: center; display: inline-block;">
    </asp:Label>

    <!-- Incorrect Feedback Label -->
    <asp:Label ID="getIncorrectFeedback" runat="server" 
               Text='<%# Eval("FeedbackIncorrect") %>'
               Visible='<%# Not String.IsNullOrEmpty(Eval("FeedbackIncorrect").ToString()) %>'
               style="background-color: #ffe6e6; color: #d32f2f; font-size: 1.1em; padding: 10px 15px; 
                      border-radius: 8px; box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); 
                      max-width: 45%; text-align: center; display: inline-block;">
    </asp:Label>
</div>



    <!-- Nested Repeater for Answer Options -->
    <div class="options-container" style="margin-top:10px;">
        <asp:Repeater ID="rptOptions" runat="server">
    <ItemTemplate>
        <div class='<%# IIf(Eval("IsCorrect"), "option-item correct", "option-item incorrect") %>' style="display:flex;flex-direction:column;justify-content:left;">
        <div>
        <span class="option-number"><%# GetRomanNumber(Container.ItemIndex + 1) %>.</span>
            <span><%# Eval("AnswerText") %> - <%# IIf(Eval("IsCorrect"), "Correct", "Incorrect") %></span>
        </div>
        <div>
        <%# If(Not String.IsNullOrEmpty(Eval("ImageOption").ToString()), 
                    "<img src='" & ResolveUrl(Eval("ImageOption").ToString()) & "' alt='Option Image' style='max-width:100px; max-height:100px; margin-top:10px;border-radius:7px;' />",
                    "") %></div>
            
            
        </div>
    </ItemTemplate>
</asp:Repeater>

    </div>
</div>

        <hr />
    </ItemTemplate>
</asp:Repeater>
    </div>
    </form>
</body>
</html>
