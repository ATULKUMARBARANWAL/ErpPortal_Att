<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddSection.aspx.vb" Inherits="Examinationjune_AddSection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>

 

    <style type="text/css">
        body {
            overflow-x: hidden;
            background-color: #f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }

        .grass5 {
            background-color: #1ed085;
            border: none;
            color: White;
            text-align: center;
            height: 40px;
            padding-top: 4px;
            border-radius: 5px; /* Rounded corners */
            transition: background-color 0.3s; /* Smooth hover effect */
        }

        .grass5:hover {
            background-color: #1aad6f;
            box-shadow: 0px 1px 5px 1px #dcdcdc;
        }

        .icongrass {
            font-size: 25px;
        }

        .Submit {
            font-size: 18px !important;
            font-weight: 500;
            height: 40px;
            cursor: pointer;
            color: White;
            background-color: #1ed085;
            border: none;
            width: 18%;
            border-radius: 5px; /* Rounded corners */
            transition: background-color 0.3s, transform 0.3s; /* Smooth hover effect */
            transform:translateX(-4rem);
            margin: 5px; /* Added margin for spacing */
        }

        .Submit:hover {
            color: White;
            background-color: #1aad6f;
            box-shadow: 0px 1px 5px 1px #dcdcdc;
                
        }

        .card-header {
            background-color: #152837;
            color: #fff;
            font-weight: 500;
            font-size: 22px;
            letter-spacing: 1px;
            margin-bottom: 2rem; /* Adjusted spacing */
            padding: 10px 15px; /* Added padding */
            border-radius: 5px; /* Rounded corners */
        }

        .labeltext {
            color: #15283c;
            font-size: 17px;
            font-weight: 450;
            letter-spacing: 1px;
        }

        .grdcourse {
            margin-right: auto;
            margin-left: auto;
            page-break-after: 20px;
        }

        .maincontainer {
            border-radius: 8px;
            padding-top: 24px;
            margin-top: 12px;
            background-color: #fff;
            padding: 12px 18px;
            box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1); /* Added box shadow */
        }

        .hiddencol1 {
            display: none;
        }

        .cap {
            width: 100%; /* Ensuring it takes full width */
        }

        .backbotton {
            font-size: 22px;
            font-weight: 600;
            color: #7c858f;
            transition: color 0.3s; /* Smooth hover effect */
        }

        .backbotton:hover {
            color: #fff;
        }

        .row {
            margin-bottom: 15px; /* Added spacing between rows */
        }

        /* Additional styles for better spacing */
        .d-flex.align-items-center {
            margin-bottom: 15px; /* Space between elements */
        }
        .container h4
        {
            padding-bottom:2rem;
            }

            .custom-alert {
        position: fixed;
        top: 100px;
        right: -350px; /* Initially hidden offscreen */
        background-color: #c00; /* Strong red background similar to the image */
        color: white;
        padding: 14px 20px; /* More padding for a compact look */
        border-radius: 3px; /* Slight rounding of corners */
        z-index: 1000;
        box-shadow: 0px 2px 8px rgba(0, 0, 0, 0.2); /* Soft shadow */
        transition: right 0.5s ease-in-out, opacity 0.5s ease; /* Smooth transition */
        opacity: 0;
        font-family: 'Arial', sans-serif;
        font-size: 20px;
        padding:10px 20px;
        display: flex;
        align-items: center; /* Vertically align content */
        justify-content: space-between; /* Space between icon/text and close button */
        width: 350px; /* Fixed width */
    }

    .custom-alert.show {
        right: 20px; /* Bring into view */
        opacity: 1;
    }

    .custom-alert i {
        margin-right: 10px;
        font-size: 18px;
    }

    .close-btn {
        margin-left: 10px;
        cursor: pointer;
        font-size: 18px;
        color: white;
    }

  table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px auto;
        }
        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: center;
        }
       
        .delete-icon {
            color: red;
            font-size: 20px;
            cursor: pointer;
        }
        .map-button {
            display: block;
            margin: 20px auto;
            padding: 10px 20px;
            background-color: #007BFF;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            
        }
        .map-button:hover {
            background-color: #0056b3;
        }
        .btnAddSectionStyle {
    background-color: white; /* Green */
    color: rgb(21, 40, 60);
      margin-left:50rem;

    border-radius: 5px;
    cursor: pointer;
    border:2px solid rgb(21, 40, 60);
    height:2.3rem;
}

.btnAddSectionStyle:hover {
    background-color: rgb(21, 40, 60); /* Darker Green */
    color:White;
}
.custom-alert {
        position: fixed;
        top: 100px;
        right: -350px; /* Initially hidden offscreen */
        background-color: #c00; /* Strong red background similar to the image */
        color: white;
        padding: 14px 20px; /* More padding for a compact look */
        border-radius: 3px; /* Slight rounding of corners */
        z-index: 1000;
        box-shadow: 0px 2px 8px rgba(0, 0, 0, 0.2); /* Soft shadow */
        transition: right 0.5s ease-in-out, opacity 0.5s ease; /* Smooth transition */
        opacity: 0;
        font-family: 'Arial', sans-serif;
        font-size: 20px;
        padding:10px 20px;
        display: flex;
        align-items: center; /* Vertically align content */
        justify-content: space-between; /* Space between icon/text and close button */
        width: 350px; /* Fixed width */
    }

    .custom-alert.show {
        right: 20px; /* Bring into view */
        opacity: 1;
    }

    .custom-alert i {
        margin-right: 10px;
        font-size: 18px;
    }

    .close-btn {
        margin-left: 10px;
        cursor: pointer;
        font-size: 18px;
        color: white;
    }

    </style>
       
<script>
    window.onload = function () {
        var pnlClassDetails = document.getElementById("<%= pnlClassDetails.ClientID %>");
        var sectionPanel = document.getElementById("<%= sectionPanel.ClientID %>");
        var btnAddSection = document.getElementById("btnAddSection");

        // Initially display only pnlClassDetails
        pnlClassDetails.style.display = "block";
        sectionPanel.style.display = "none";

        // Add event listener to btnAddSection
        btnAddSection.addEventListener("click", function () {
            pnlClassDetails.style.display = "none";
            sectionPanel.style.display = "block";
        });
    }
</script>
 <script>
     function showCustomAlert(message) {
         var alertBox = document.getElementById('customAlert');
         var alertMessage = document.getElementById('customAlertMessage');
         var alertIcon = alertBox.querySelector('i'); // Assuming there's an <i> tag for the icon

         // Set the custom message
         alertMessage.innerHTML = message;

         // Check if message contains the word "success"
         if (message.toLowerCase().includes('success')) {
             alertBox.style.backgroundColor = '#28a745'; // Green background
             alertIcon.className = 'fa fa-check-circle'; // Update icon to right-tick
         } else {
             alertBox.style.backgroundColor = '#c00'; // Default red background
             alertIcon.className = 'fa fa-exclamation-triangle'; // Default warning icon
         }

         // Make the alert visible with transition
         alertBox.style.display = 'flex'; // Ensure display is flex to align items
         setTimeout(function () {
             alertBox.classList.add('show'); // Add class to slide it in
         }, 100); // Delay for smooth transition

         // Hide the alert after 3 seconds
         setTimeout(function () {
             alertBox.classList.remove('show'); // Slide it out
             setTimeout(function () {
                 alertBox.style.display = 'none'; // Hide the alert completely
             }, 500); // Wait for the slide-out animation to complete
         }, 3000); // Show for 3 seconds
     }

     // Function to manually close the alert
     function closeCustomAlert() {
         var alertBox = document.getElementById('customAlert');
         alertBox.classList.remove('show'); // Slide it out
         setTimeout(function () {
             alertBox.style.display = 'none'; // Hide the alert completely
         }, 500); // Wait for the slide-out animation to complete
     }
</script>
<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        // Get the search bar input element
        var searchBar = document.getElementById("searchBar");

        // Listen for input events on the search bar
        searchBar.addEventListener("input", function () {
            // Get the search term entered by the user
            var searchTerm = searchBar.value.toLowerCase();

            // Get all rows in the GridView
            var gridRows = document.querySelectorAll("#gvClassDetails tr");

            // Loop through all rows, skipping the header row (index 0)
            for (var i = 1; i < gridRows.length; i++) {
                var row = gridRows[i];
                var sectionCell = row.cells[2]; // The "Section" field is in the 3rd column (index 2)

                // If the sectionCell exists, check if the section code matches the search term
                if (sectionCell) {
                    var sectionText = sectionCell.textContent || sectionCell.innerText;

                    // If the section text contains the search term, show the row, otherwise hide it
                    if (sectionText.toLowerCase().includes(searchTerm)) {
                        row.style.display = ""; // Show the row
                    } else {
                        row.style.display = "none"; // Hide the row
                    }
                }
            }
        });
    });
</script>



</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="container-fluid">
                 <div id="customAlert" class="custom-alert">
    <i class="fa fa-exclamation-triangle"></i>
    <span id="customAlertMessage">This is a custom alert!</span>
    <span class="close-btn" onclick="closeCustomAlert()">&times;</span>
</div>




            <div class="row justify-content-center mt-1">
                <div class="col-md-12">
                    <div class="card maincontainer">
                    
                        <div class="container mt-3">
                     
                        <div style="display:flex;justify-content:space-around;align-items:center;border-bottom: 1px solid rgb(225, 226, 227);">
                           <div class="text-center maincontainerm m-0" style="transform:translateX(-15px);">
        <asp:LinkButton ID="backbotton" runat="server" ><i class="fa-solid fa-arrow-left" style="color:rgb(45, 55, 64);font-size:20px;"></i></asp:LinkButton>
     </div>                             
                        <h4 style="color:rgb(33, 37, 41); padding-bottom: 15px; margin-top:18px;width:100%;">Add Section</h4>
                        
                                
<p style="margin-left:-3rem; width:30%;">Academic Year: <span class="fw-bold">
    <%: ViewState("Academicyear") %>
</span></p>

                                
                        </div>
                           <div class="row d-flex align-items-center justify-content-between mt-4" style="border-bottom: 1px solid rgb(225, 226, 227); padding-bottom: .5rem;">
    <div class="col-md-4">
        <div class="d-flex align-items-center">
            <label for="ddlProgram" class="me-2">Class:</label>
            <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                
            </asp:DropDownList>
        </div>
    </div>

    <div class="col-md-4 text-end" style="margin-top:-1rem;">
        <div class="d-flex">
            <label for="searchBar"  class="me-2 mt-2">Search:</label>
            <input type="text" id="searchBar" class="form-control" placeholder="Search..." />
        </div>
    </div>
</div>


<asp:Panel ID="pnlClassDetails" runat="server" visible="true">
<div>
                                           <small  >Instructions <i class="fa-solid fa-circle-info"></i> :-
                                                <i class="fa-solid fa-star" style="color: #1ed085;"></i>Add New Section by clicking on"+Add Section".
                                                <i class="fa-solid fa-star" style="color: #1ed085;"></i>"Delete Perticuler Section By Clicking on"X" 
                        
                                            </small>
                                        </div>
   <asp:GridView ID="gvClassDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
    OnRowCommand="gvClassDetails_RowCommand" DataKeyNames="Classid,ClassesID" ShowHeaderWhenEmpty="True">
    <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ClassName" HeaderText="Class Name" />
        <asp:BoundField DataField="Classid" HeaderText="Class ID" Visible="False" />
        <asp:BoundField DataField="code" HeaderText="Section" SortExpression="code">
            <ItemStyle Font-Bold="True" />
        </asp:BoundField>
        <asp:BoundField DataField="ClassesID" HeaderText="Section ID" Visible="false" />
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Remove" CommandArgument='<%# Container.DataItemIndex %>'
                    ImageUrl="https://cdn3.iconfinder.com/data/icons/softwaredemo/PNG/256x256/DeleteRed.png"
                    Width="30px" Height="30px" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <table>
            <tr>
                <td colspan="5" style="text-align:center; font-weight:bold;">
                    No data available.
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
</asp:GridView>







    <asp:Button ID="btnAddSection" runat="server" Text="+ Add Section" CssClass="btnAddSectionStyle" OnClientClick="return false;" />

</asp:Panel>

<asp:Panel ID="sectionPanel" runat="server" >
		
    <div class="card-body">
    <div>
                                          <small>Instructions <i class="fa-solid fa-circle-info"></i>:
<i class="fa-solid fa-star" style="color: #1ed085;"></i> You can add sections by selecting the "Checkbox" option.
<i class="fa-solid fa-star" style="color: #1ed085;"></i> A search option is also available.
</small>
                                        </div>
										
       <asp:GridView ID="gvSections" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="ClassesID">
    <Columns>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Code" HeaderText="Section" />
    </Columns>
</asp:GridView>




       <asp:Button style="background-color:rgb(30, 208, 133);border:none;width:9rem;margin-left:40%;" ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />

    </div>
</asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
