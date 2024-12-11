<%@ Page Language="VB" AutoEventWireup="false" CodeFile="leaveApproval.aspx.vb" Inherits="Attendance_leaveApproval" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Assign Class Teacher</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://kit.fontawesome.com/a429e5bfb6.js" crossorigin="anonymous"></script>
    <style>
       
    .form-select {
    width: 200px; /* Adjust the width as needed */
    padding: 8px;
    font-size: 14px;
    border: 1px solid #ced4da;
    border-radius: 4px;
    background-color: #fff;
    color: #495057;
}

.btn {
    padding: 8px 12px;
    font-size: 14px;
    border-radius: 4px;
    border: none;
}

.btn-success {
    background-color: #28a745;
    color: #fff;
}

.btn-success:hover {
    background-color: #218838;
    color: #fff;
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
        function filterGridView() {
            // Get the search term from the input box
            var searchValue = document.getElementById("search").value.toLowerCase();

            // Get the GridView table
            var gridView = document.getElementById("<%= GridView1.ClientID %>");
            var rows = gridView.getElementsByTagName("tr"); // All rows in the GridView

            // Loop through each row (skip the header row)
            for (var i = 1; i < rows.length; i++) {
                var studentCell = rows[i].cells[2]; // Column index for "Student Name"
                var leaveTypeCell = rows[i].cells[6]; // Column index for "Leave Type"

                if (studentCell && leaveTypeCell) {
                    var studentText = studentCell.innerText.toLowerCase();
                    var leaveTypeText = leaveTypeCell.innerText.toLowerCase();

                    // Check if search value matches "Student Name" or "Leave Type"
                    if (studentText.includes(searchValue) || leaveTypeText.includes(searchValue)) {
                        rows[i].style.display = ""; // Show row
                    } else {
                        rows[i].style.display = "none"; // Hide row
                    }
                }
            }
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

</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4" >
           <div id="customAlert" class="custom-alert">
    <i class="fa fa-exclamation-triangle"></i>
    <span id="customAlertMessage">This is a custom alert!</span>
    <span class="close-btn" onclick="closeCustomAlert()">&times;</span>
</div>

            <!-- Header Section -->
            <div class="d-flex justify-content-between align-items-center mb-4" >
                <h3>Approve Student Leave</h3>
                  <div class="col-md-4  d-flex align-items-center gap-2 "style="margin-right:-4rem;">
        <label for="ddlClass" class="form-label">Class</label>
        <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" class="form-select">
</asp:DropDownList>

    </div>
            </div>

            <hr />
           <div style="margin-bottom:8px;">
                                          <small>Instructions <i class="fa-solid fa-circle-info"></i>:
<i class="fa-solid fa-star" style="color: #1ed085;"></i> Approve or reject leave requests by selecting an option from the "Dropdown" and clicking the "Change Status" button.
<i class="fa-solid fa-star" style="color: #1ed085;"></i> You can also perform bulk updates to the "Status" and check which students have pending, rejected, or approved statuses.
</small>
                                        </div>    
            <div class="row mb-3 " style="display:flex;">
<div class="col-md-4">
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem Text="Pending" Value="Pending" />
        <asp:ListItem Text="Approve" Value="Approve" />
        <asp:ListItem Text="Reject" Value="Reject" />
    </asp:DropDownList>
</div>

   
   <div class="col-md-4 d-flex align-items-center gap-2"style="margin-left:20rem;">
    <label for="search" class="form-label mb-0">Search</label>
      <input 
        type="text" 
        id="search" 
        class="form-control" 
        placeholder="Search by Student or Leave Type..." 
        style="width:16rem;" 
        onkeyup="filterGridView()" />
</div>

    
 
</div>
<hr />
<div style="display: flex; justify-content: flex-start; align-items: center; gap: 10px; margin-bottom:1.5rem;">


    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" style="width:12rem;">
        <asp:ListItem Text="Approve" Value="Approve" />
        <asp:ListItem Text="Reject" Value="Reject" />
    </asp:DropDownList>
     <asp:TextBox 
    style="width:15rem;" 
    ID="txtRemark" 
    runat="server" 
    CssClass="form-control" 
    Placeholder="Add Remark">
</asp:TextBox>
    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success" Text="Change Status" OnClick="btnApprove_Click" />
  
</div>

    

                                        
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
    DataKeyNames="studentID" OnRowDataBound="GridView1_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" />
            </ItemTemplate>
            <HeaderTemplate>
                <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="True" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ClassName" HeaderText="Class" />
        <asp:BoundField DataField="code" HeaderText="Section" />
        <asp:BoundField DataField="student" HeaderText="Student Name" />
        <asp:BoundField DataField="applyDate" HeaderText="Apply Date" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="fromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="toDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="leaveType" HeaderText="Leave Type" />
        <asp:BoundField DataField="reason" HeaderText="Reason" />
        <asp:TemplateField HeaderText="Download">
            <ItemTemplate>
               
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="status" HeaderText="Status" />
        <asp:BoundField DataField="Remark" HeaderText="Remark" />
    </Columns>
    <EmptyDataTemplate>
        <div class="alert alert-info">No data available</div>
    </EmptyDataTemplate>
</asp:GridView>
     </div>
    </form>
    <!-- Bootstrap JS (Optional) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
