<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssignClassTeacher.aspx.vb" Inherits="Examinationjune_AssignClassTeacher" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Assign Class Teacher</title>
    <style>
    body {
        font-family: 'Arial', sans-serif;
        background-color: #f5f5f5; /* Light background for contrast */
    }

    .container {
        width: 62rem;
        margin: 0 auto;
        margin-top: 10px;
        background: white;
        padding: 1rem 3rem 2.5rem 3rem;
        border-radius: 7px;
        box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.1); /* Soft shadow for depth */
    }

  

    label {
        font-weight: bold;
    }

    .form-control {
        width: 100%;
        padding: 8px;
        margin-bottom: 10px;
       
        border-radius: 5px;
       
        
    }

  
    table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 20px;
        background: white;
    }

    table, th, td {
        border: 1px solid #ddd;
    }

    th {
       
        font-weight: bold;
    }

    th, td {
        padding: 10px;
        text-align: left;
    }

  
    select {
        width: 100%;
        padding: 8px;
        margin-bottom: 10px;
       
        border-radius: 5px;
    }

  

    .save-btn {
        display: inline-block;
        background-color: rgb(30, 208, 133);
        color: white;
        padding: 10px 20px;
        border: none;
        cursor: pointer;
        border-radius: 5px;
        font-size: 16px;
        text-align: center;
        transition: background-color 0.3s ease, box-shadow 0.3s ease; /* Smooth hover effect */
    }

    .save-btn:hover {
        background-color: #218838;
        box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.2); /* Elevated shadow on hover */
    }

    .header-section {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 20px;
        border-bottom: 1px solid rgb(225, 226, 227);
        padding-bottom: 10px;
    }

    .dropdown-section {
        display: inline-block;
        width: 20rem;
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
        padding: 10px 20px;
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

    /* Additional styles for better readability and spacing */
    h2 {
        font-size: 2rem;
        color: #333;
        text-align: center;
        margin-bottom: 30px;
     
        display: inline-block;
        padding-bottom: 5px;
    }

</style>
<script src="https://kit.fontawesome.com/a429e5bfb6.js" crossorigin="anonymous"></script>
<script>
document.addEventListener("DOMContentLoaded", function () {
    const searchBar = document.getElementById("searchBar");

    // Attach event listener to the search bar
    searchBar.addEventListener("input", function () {
        const searchTerm = searchBar.value.toLowerCase();
        const tableRows = document.querySelectorAll("tbody tr"); // Select all rows in the table body

        tableRows.forEach((row) => {
            const codeCell = row.querySelector("td:nth-child(3)"); // The 3rd column: "Section (Code)"
            const dropdownCell = row.querySelector("td:nth-child(5) select"); // The 5th column: DropDownList

            const codeText = codeCell ? (codeCell.textContent || codeCell.innerText).toLowerCase() : "";
            const dropdownText = dropdownCell ? dropdownCell.options[dropdownCell.selectedIndex].text.toLowerCase() : "";

            // Check if the search term matches either column
            if (codeText.includes(searchTerm) || dropdownText.includes(searchTerm)) {
                row.style.display = ""; // Show matching rows
            } else {
                row.style.display = "none"; // Hide non-matching rows
            }
        });
    });
});

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
    <div class="container">
     <div id="customAlert" class="custom-alert">
    <i class="fa fa-exclamation-triangle"></i>
    <span id="customAlertMessage">This is a custom alert!</span>
    <span class="close-btn" onclick="closeCustomAlert()">&times;</span>
</div>
<div style='border-bottom: 1px solid rgb(225, 226, 227); display:flex;align-items:center;justify-content:space-between;'>
<div style="display:flex;gap:8px;margin-top:20px;">
<asp:LinkButton ID="backbotton" runat="server" style="margin-top:1rem;" ><i class="fa-solid fa-arrow-left" style="color:rgb(45, 55, 64);font-size:30px;"></i></asp:LinkButton>
<h3 style="color:rgb(33, 37, 41);padding-bottom: 15px; ">Assign Class Teacher</h3>
</div>
    
                                        <div class="col-md-4" style = "right:10px">
                                          &nbsp;&nbsp; <small > Instructions !<br /> 
                                                <i class="fa-solid fa-star" style="color: #1ed085;"></i>Before Initialize Class Teacher Firstly Assign Section</br>   
                        
                                            </small>
                                        </div>
                                  
             
             </div>
        <div class="header-section" style="margin-top:2rem;">
       
            <div class="dropdown-section">
                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                    <asp:ListItem Text="Select Class" Value="0"></asp:ListItem> 
                </asp:DropDownList>
            </div>
          <div class="col-md-4 text-end" style="margin-top:-1rem;">
        <div style="display:flex;margin-top:12px;gap:6px;">
            <label for="searchBar"  class=" mt-3"style="margin-top:8px;">Search:</label>
            <input type="text" id="searchBar" class="form-control" placeholder="Search..." />
        </div>
    </div>
 
        </div>
        <div class="col-md-10">
                                          <small>Instructions <i class="fa-solid fa-circle-info"></i>:
<i class="fa-solid fa-star" style="color: #1ed085;"></i> First, assign the "Section" before assigning the "Class Teacher."
<i class="fa-solid fa-star" style="color: #1ed085;"></i> Assigning a "Class Teacher" is essential.
</small>
                                        </div>
        <table>
    <thead>
        <tr>
            <th>S.No.</th>
            <th>Class</th>
            <th>Section</th>
            <th>Total Student</th>
            <th>Class Teacher Name</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="RepeaterSection" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Container.ItemIndex + 1 %></td> <!-- Serial Number -->
                    <td>
                        <%# Eval("ClassName") %>
                        <asp:HiddenField ID="hfClassId" runat="server" Value='<%# Eval("Classid") %>' />
                    </td>
                    <td>
                        <%# Eval("Code") %>
                        <asp:HiddenField ID="hfClassesID" runat="server" Value='<%# Eval("ClassesID") %>' />
                    </td>
                    <td>
                        <asp:Literal ID="ltStudentCount" runat="server" Text='<%# Eval("StudentCount") %>'></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFaculity" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Faculty" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>


        <div style="text-align: center; margin-top: 20px;">
            <asp:Button ID="btnSave" runat="server" Text="Add Class Teacher" CssClass="save-btn" OnClick="btnSave_Click" />
        </div>
    </div>
</form>

</body>
</html>
