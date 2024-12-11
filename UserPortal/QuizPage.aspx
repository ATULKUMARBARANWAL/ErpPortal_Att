<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QuizPage.aspx.vb" Inherits="UserPortal_QuizPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quiz Page</title>
    <style>
        /* Fullscreen styles */
        body, html {
            width: 100%;
            height: 100%;
            margin: 0;
            padding:20px 20px;
            overflow: hidden;
            
           
            background-color: #f8f9fa;
        }
        /* Modal overlay styles */
        #fullscreenPrompt {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 1);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
            color: #333;
        }
        #modalContent {
            background-color: #ffffff;
            padding: 30px;
            border-radius: 8px;
            max-width: 600px;
            text-align: center;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
            position: relative;
        }
        #modalContent h2 {
            color: #007bff;
            margin-bottom: 15px;
            font-size: 1.8em;
        }
        #modalContent ul {
            list-style-type: disc;
            padding-left: 20px;
            text-align: left;
            margin-bottom: 20px;
        }
        #modalContent ul li {
            margin-bottom: 10px;
            font-size: 1em;
        }
        #modalContent .modal-buttons {
            display: flex;
            justify-content: space-between;
        }
        #modalContent button {
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        #startQuizButton {
            background-color: #28a745;
            color: #ffffff;
        }
        #cancelButton {
            background-color: #dc3545;
            color: #ffffff;
        }
        /* Close icon */
        .close-icon {
            position: absolute;
            top: 10px;
            right: 10px;
            font-size: 20px;
            color: #888;
            cursor: pointer;
        }
    </style>
    <style>


h2 {
    text-align: center;
}

.navigation-buttons {
    display: flex;
    gap:10px;
    margin-top: 20px;
}

.btn {
    padding: 8px 16px;
    background-color: #007bff;
    color: #fff;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    text-align: center;
}

.btn:disabled {
    background-color: #ccc;
    cursor: not-allowed;
}


    </style>
        <style>
        .quiz-container {
            max-width: 80%;
            margin: auto;
            padding: 20px;
            background: #fff;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin-left: 100px;
            height: 750px;
        }

        h2 {
            font-size: 2em;
            color: #333;
            margin-bottom: 10px;
        }

        .quiz-details {
            display: flex;
            justify-content: space-between;
            padding: 10px 0;
            border-bottom: 1px solid #ccc;
        }

        .quiz-timer, .quiz-points {
            font-size: 1.1em;
            color: #333;
        }

        .questions-section {
            display: flex;
            justify-content: space-between;
        }

        .question-content {
            width: 70%;
        }

        .question {
            margin-bottom: 20px;
        }

        .options {
            margin-left: 20px;
        }

        .options input {
            margin-right: 8px;
        }

        .quiz-navigation {
            width: 15%;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 8px;
        }

        .quiz-navigation h3 {
            font-size: 1.2em;
            margin-bottom: 10px;
        }

        .navigation-buttons {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }

        .navigation-buttons .nav-button {
            width: 40px;
            height: 40px;
            border: 1px solid #1a73e8;
            color: #1a73e8;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            border-radius: 4px;
        }

        .navigation-buttons .nav-button:hover {
            background-color: #1a73e8;
            color: #fff;
        }

        .action-buttons {
            display: flex;
            margin-top: 20px;
        }

        .action-buttons .btn {
            padding: 10px 20px;
            font-size: 1em;
            border: 1px solid #4CAF50;
            color: #4CAF50;
            background-color: white;
            border-radius: 4px;
            cursor: pointer;
        }

        .action-buttons .btn:hover {
            background-color: #4CAF50;
            color: #fff;
        }

        .btn[disabled] {
            background-color: #ccc;
            color: #666;
            cursor: not-allowed;
        }
        .questions-section {
        display: flex;
        gap: 20px;
    }
    .question-content {
        flex: 3;
        padding: 20px;
    }
    .quiz-navigation {
        flex:1;
        padding: 20px;
        height:auto;
        border-left: 1px solid #ddd;
    }
    .nav-button {
        display: inline-block;
        width: 40px;
        height: 40px;
        margin: 5px;
        text-align: center;
        line-height: 40px;
        background-color: #f0f0f0;
        border: 1px solid #ccc;
        cursor: pointer;
    }
    .navigation-controls {
        margin-top: 20px;
        display: flex;
        justify-content: space-between;
    }
    .btn-navigation, .btn-submit {
        padding: 10px 20px;
        font-size: 16px;
        border: 1px solid #28a745;
        background-color: #fff;
        cursor: pointer;
    }
    .btn-navigation:hover, .btn-submit:hover {
        background-color: #28a745;
        color: #fff;
    }
    .question-image {
    width: 10%;
    height: auto;
    margin-top: 10px;
    margin-bottom:20px;
    border-radius: 8px;
    border:2px solid gray;
}

.option-image {
    width: 25%;
    height: auto;
    margin-left: 10px;
    border-radius: 4px;
}

.option {
    display: flex;
    align-items: center;
    margin-bottom: 10px;
    padding: 8px;
    border-radius: 5px;
    background-color: white;
    transition: background-color 0.3s ease;
}

.option:hover {
    background-color: white;
}
.options-table {
    width: 100%;
    margin-top: 15px;
}

.options-table td {
    padding: 10px;
    vertical-align: top;
    width: 50%; /* Ensures two columns take up the full width */
}

.option-image {
    display: inline-block;
    margin-left: 20px;
    max-width: 25%;
    margin-top:10px;
    height: auto;
    vertical-align: middle;
}

    </style>
    <script>
        window.onload = function() {
            let totalTime = parseInt(document.getElementById("timeRemaining").innerText) * 60;

            function startTimer() {
                const timerInterval = setInterval(function() {
                    const minutes = Math.floor(totalTime / 60);
                    const seconds = totalTime % 60;

                    document.getElementById("timeRemaining").innerText = `${minutes}m ${seconds < 10 ? '0' : ''}${seconds}s`;

                    if (totalTime <= 0) {
                        clearInterval(timerInterval);
                        alert("Time's up!");
                        document.getElementById('<%= btnSubmit.ClientID %>').click();
                    }
                    totalTime--;
                }, 1000);
            }

            startTimer();
        };
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div id="fullscreenPrompt">
            <div id="modalContent">
                <span class="close-icon" onclick="cancelQuiz();">&times;</span>
                <h2>Quiz Instructions</h2>
                <ul>
                    <li>Make sure you have a stable internet connection.</li>
                    <li>You must complete the quiz in one sitting.</li>
                    <li>No external help is allowed during the quiz.</li>
                    <li>Each question has a time limit, so manage your time wisely.</li>
                    <li>Do not refresh the page during the quiz.</li>
                    <li>Once submitted, answers cannot be changed.</li>
                    <li>Your results will be shown at the end of the quiz.</li>
                    <li>Good luck and do your best!</li>
                </ul>
                <div class="modal-buttons">
                    <button id="startQuizButton" onclick="startQuiz(); return false;">Start Quiz</button>
                    <button id="cancelButton" onclick="cancelQuiz(); return false;">Cancel</button>
                </div>
            </div>
        </div>

      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      
        <div class="quiz-container" >
        <div id="quizContainer" runat="server">
        
       
            <h2><asp:Literal ID="litQuizTitle" runat="server"></asp:Literal></h2>

            <!-- Quiz Details -->
            <div class="quiz-details">
                <div class="quiz-timer">
                    <strong>Time Remaining:</strong> <span id="timeRemaining"><asp:Literal ID="litQuizDuration" runat="server"></asp:Literal></span>
                </div>
                <div class="quiz-points">
                    <strong>Total Points:</strong> <asp:Literal ID="litQuizTotalPoints" runat="server"></asp:Literal>
                </div>
               
                <asp:HiddenField ID="hfCurrrentOptions" runat="server" />
                <%--<asp:HiddenField ID="OptionHiddenField1" runat="server" />
                <asp:HiddenField ID="OptionHiddenField2" runat="server" />
                <asp:HiddenField ID="OptionHiddenField3" runat="server" />
                <asp:HiddenField ID="OptionHiddenField4" runat="server" />--%>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <!-- Questions Section -->
        <div class="questions-section" style="height:550px;padding:10px;">
            <div class="question-content">
             <asp:HiddenField ID="hfCurrentQuestionID" runat="server" />
                <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestions_ItemDataBound">
    <ItemTemplate>

    <asp:HiddenField ID="hfSavedResponse" runat="server" />

        <div class="question">
            <strong> <span style="font-size:20px;"><%# Eval("QuestionText") %></span></strong><br />
            
            <!-- Display Question Image if available -->
            <asp:Image ID="imgQuestion" runat="server" ImageUrl='<%# Eval("ImageQuestion") %>' CssClass="question-image" Visible='<%# Not String.IsNullOrEmpty(Eval("ImageQuestion").ToString()) %>' />

            
             <asp:HiddenField ID="hfQuestionType" runat="server" Value='<%# Eval("QuestionType") %>' />

           
<asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
    <ItemTemplate>
        <div class="option" style="display:flex;">
            <asp:RadioButton ID="rbOption" runat="server"
                             Text='<%# Eval("AnswerText") %>' 
                             Visible='<%# Eval("QuestionType").ToString() = "MCQ" OrElse Eval("QuestionType").ToString() = "TF" %>'
                             AutoPostBack="True" OnCheckedChanged="rbOption_CheckedChanged" />
            <asp:CheckBox ID="cbOption" runat="server" 
                          Text='<%# Eval("AnswerText") %>' 
                          Visible='<%# Eval("QuestionType").ToString() = "MultiCorrect" %>'
                          AutoPostBack="True" OnCheckedChanged="cbOption_CheckedChanged" />
            <asp:Image ID="imgOption" runat="server" ImageUrl='<%# Eval("ImageOption") %>' CssClass="option-image" style="height:auto; width:10%;" Visible='<%# Not String.IsNullOrEmpty(Eval("ImageOption").ToString()) %>' />
            <asp:HiddenField ID="hfAnswerID" runat="server" Value='<%# Eval("AnswerID") %>' />
        </div>
    </ItemTemplate>
</asp:Repeater>




        </div>
    </ItemTemplate>
</asp:Repeater>

            </div>

            <!-- Quiz Navigation Panel -->
            <div class="quiz-navigation">
                <h3>Quiz Navigation</h3>
                <hr />
                <div class="navigation-buttons">
                    <asp:Repeater ID="quizNavRepeater" runat="server" OnItemCommand="quizNavRepeater_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnNavigate" runat="server" CssClass="nav-button"
                                Text='<%# Container.ItemIndex + 1 %>' CommandArgument='<%# Container.ItemIndex + 1 %>'
                                CommandName="NavigateToQuestion" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <!-- Action Buttons -->
        <div class="action-buttons" style="display:flex;margin-left:600px;">
            <asp:Button ID="btnPrevious" runat="server" CssClass="btn" Text="Previous" OnClick="btnPrevious_Click" Enabled="False" /> &nbsp;
            <asp:Button ID="btnNext" runat="server" CssClass="btn" Text="Next" OnClick="btnNext_Click" style="margin-left:10px;" />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" Visible="False" style="margin-left:10px;" OnClick="btnSubmit_Click"/>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

 </div>
        </div>
         


<%--         <script type="text/javascript">
             function storeOptionAndQuestionIDs(questionID, optionID) {
                 // Set the current question ID if not already set
                 var hfCurrentQuestionID = document.getElementById('<%= hfCurrentQuestionID.ClientID %>');
                 if (!hfCurrentQuestionID.value) {
                     hfCurrentQuestionID.value = questionID;
                 }

                 // Store the selected option ID in the respective hidden field
                 document.getElementById('<%= hfCurrrentOption1ID.ClientID %>').value = optionID;
             }

             // Set Question ID on initial load if it is not already set
             window.onload = function () {
                 var hfCurrentQuestionID = document.getElementById('<%= hfCurrentQuestionID.ClientID %>');
                 if (!hfCurrentQuestionID.value) {
                     hfCurrentQuestionID.value = "<%= hfCurrentQuestionID.Value %>";
                 }
             };
</script>--%>


        <script>
            const elem = document.documentElement;

            // Start quiz in fullscreen mode
            function startQuiz() {
                if (elem.requestFullscreen) {
                    elem.requestFullscreen().then(() => {
                        document.getElementById('fullscreenPrompt').style.display = 'none';
                    }).catch((err) => {
                        console.log('Fullscreen request failed:', err);
                    });
                }
            }

            // Cancel quiz and go back to the previous page
            function cancelQuiz() {
                window.history.back();
            }

            // Detect fullscreen change (for Esc key and native exit button)
            document.addEventListener('fullscreenchange', () => {
                if (!document.fullscreenElement) {
                    // If fullscreen is exited, navigate back to the previous page
                    window.history.back();
                }
            });
        </script>
    </form>
</body>
</html>
