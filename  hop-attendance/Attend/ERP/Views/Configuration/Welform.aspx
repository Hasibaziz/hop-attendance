﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<ERP.Domain.Model.WelformEntity>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Welform
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="mp_left_menu">
        <% Html.RenderPartial("LeftMenu"); %>
    </div>
    <div class="mp_right_content">
            <div class="page_list_container">
            <div><%--<div> Welform Management</div>--%>
                Start Date: <%: Html.TextBoxFor(m => m.StartDate, new { @readonly = "true", @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                End Date: <%: Html.TextBoxFor(m => m.EndDate, new {@readonly = "true",  @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                <input type="button" value="Show" title="Save"  id="GetAttenList" /> &nbsp; &nbsp;&nbsp;
                <input type="button" value="Export to Excel" title="Export"   onclick="BTNALL()" />
                <%--<input type="button" value="Print Report" title="Save"   onclick="printItem()" />--%>
            </div>
            <div id="RecordsContainer">
            </div>
        </div>
    </div>
<%--<script type="text/javascript">
        function printItem() {
            alert("Report");
            window.open("/Configuration/ReportView01");
        }
</script>--%>
<script type="text/javascript">
    function BTNALL() {
        startDate = $("#StartDate").val();
        endDate = $("#EndDate").val();

        window.location = "/Configuration/WelExcelReport?startDate=" + startDate + "&endDate=" + endDate;
    }
</script>
<script type="text/javascript">
        $(document).ready(function () {

            $('#RecordsContainer').jtable({
                paging: true,
                pageSize: 30,
                sorting: false,
                title: ' Welform Management ',
                defaultSorting: 'Name ASC',
                actions: {
                    listAction: '<%=Url.Content("~/Configuration/WelformInfoList") %>'                   
                },
                fields: {
//                    Id: {
//                        key: true,
//                        create: false,
//                        edit: false,
//                        list: false
//                    },
                    PDate: {
                        title: 'Date',
                        width: '5%',
                        //type: 'date',
                        displayFormat: 'dd-mm-yy'
                    },
                    EMPID: {
                        title: 'Emp ID',
                        width: '8%'
                    },
                    EName: {
                        title: 'EMP Name',
                        width: '20%'
                    },
                    Designation: {
                        title: 'Designation',
                        width: '10%'
                    },
                    DeptName: {
                        title: 'Department',
                        width: '10%'
                    },
                    Intime: {
                        title: 'Intime',
                        width: '5%'
                    },
                    Outtime: {
                        title: 'Outtime',
                        width: '5%'
                    },
                    Status: {
                        title: 'Status',
                        width: '5%'
                    }
                }
            });
            $('#GetAttenList').click(function (e) {
                e.preventDefault();
                $('#RecordsContainer').jtable('load', {
                    StartDate: $('#StartDate').val(),
                    EndDate: $('#EndDate').val()
                });
            });
        });
 
    </script>
<script type="text/javascript">

    $(function () {
        $(".datefield").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: '-100y:c+nn',
            maxDate: '1d'
        });
    });

</script>

</asp:Content>
