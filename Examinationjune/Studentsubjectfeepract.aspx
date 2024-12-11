<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Studentsubjectfeepract.aspx.vb" Inherits="Examinationjune_Studentsubjectfeepract" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Subject Fee</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container {
            max-width: 1200px; /* Increase the form width */
        }

        .form-container {
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            margin-top: 20px;
            border: 1px solid #dee2e6; /* Add border */
        }

        .filter-container {
            padding: 20px;
            border: 1px solid #dee2e6; /* Add border */
            border-radius: 10px;
            background-color: #fff;
            margin-bottom: 20px;
        }

        .table-container {
            border: 1px solid #dee2e6; /* Add border */
            border-radius: 10px;
            overflow-x: auto; /* Enable horizontal scrolling */
        }

        .save-button {
            text-align: center;
            margin-top: 20px;
        }

        h3 {
            margin-bottom: 20px;
        }

        /* Media queries for responsiveness */
        @media (max-width: 768px) {
            .form-container {
                padding: 15px;
            }

            .filter-container {
                padding: 15px;
            }

            .save-button {
                text-align: center;
            }

            .table-container {
                margin-top: 10px;
            }
        }
    </style>
    <script type="text/javascript">
        function calculateTotal(sender) {
            // Get the current row
            var row = sender.parentNode.parentNode;

            // Get the input fields
            var subjectFee = parseFloat(row.querySelector('[id$=txtSubjectFee]').value) || 0;
            var practicalFee = parseFloat(row.querySelector('[id$=txtPracticalFee]').value) || 0;
            var absentFine = parseFloat(row.querySelector('[id$=txtAbsentFine]').value) || 0;
            var fileFine = parseFloat(row.querySelector('[id$=txtFileFine]').value) || 0;

            // Calculate total
            var totalCollection = subjectFee + practicalFee + absentFine + fileFine;

            // Update the total collection label
            row.querySelector('[id$=lblTotalCollection]').innerText = totalCollection.toFixed(2);
        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
        <div class="container">
            <div class="form-container">
                <h3>Subject Fee</h3>
                <div class="filter-container">
                    <div class="row">
                        <div class="col-md-4 col-sm-12">
                            <label for="ddlClass">Select Class:</label>
                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="True" ></asp:DropDownList>
                        </div>
                        <div class="col-md-4 col-sm-12">
                            <label for="ddlSemester">Select Part:</label>
                            <asp:DropDownList ID="Ddlsemyear" CssClass="form-control" AutoPostBack="true"  runat="server" OnSelectedIndexChanged="Ddlsemyear_SelectedIndexChanged">
                          
                              </asp:DropDownList> </div>
                        <div class="col-md-4 col-sm-12">
                            <label for="ddlSubject">Select Subject:</label>
                            <asp:DropDownList ID="DdlSubject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlSubject_SelectedIndexChanged1">
                              </asp:DropDownList> </div>
                        </div>
                    </div>
                </div>

                <div class="table-container">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSubjectFees" runat="server" CssClass="table table-bordered table-hover text-center" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' CssClass="form-control text-center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- %><asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfSubjectId" runat="server" Value='<%# Eval("SubjectId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:BoundField DataField="StudentId" HeaderText="Student ID"/>
                                <asp:BoundField DataField="Student" HeaderText="Student Name"/>

                                <asp:TemplateField HeaderText="Subject Fee">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSubjectFee" runat="server" Text='<%# Bind("SubjectFee") %>' CssClass="form-control text-center" OnBlur="calculateTotal(this)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Practical Fee">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPracticalFee" runat="server" Text='<%# Bind("PracticalFee") %>' CssClass="form-control text-center" OnBlur="calculateTotal(this)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Absent Fine">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAbsentFine" runat="server" Text='<%# Bind("AbsentFine") %>' CssClass="form-control text-center" OnBlur="calculateTotal(this)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Fine">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFileFine" runat="server" Text='<%# Bind("FileFine") %>' CssClass="form-control text-center" OnBlur="calculateTotal(this)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--    <asp:TemplateField HeaderText="Total Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalCollection" runat="server" Text='<%# Bind("TotalCollection") %>' CssClass="form-control text-center"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <div class="save-button">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div>
        
        <br />
    </form>
</body>
</html>
