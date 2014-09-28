<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<ERP.Domain.Model.ManagerdaycalEntity>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Managerdaycal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="mp_left_menu">
        <% Html.RenderPartial("SLeft"); %>
    </div>
    <div class="mp_right_content">
            <div class="page_list_container">
            <div><%--<div> Managers Attendance Summary </div>--%>
                Start Date: <%: Html.TextBoxFor(m => m.StartDate, new { @readonly = "true", @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                End Date: <%: Html.TextBoxFor(m => m.EndDate, new {@readonly = "true",  @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                <input type="button" value="Show" title="Save"  id="GetAttenList" /> &nbsp; &nbsp;&nbsp;
                <%--<input type="button" value="Print Report" title="Print"   onclick="printItem()" />--%>
                <input type="button" value="Save As Excel" title="Excel"   onclick="excel_btn()" />
            </div>
            <div id="RecordsContainer">
            </div>
        </div>
    </div>
<script type="text/javascript">
    function excel_btn() {
        startDate = $("#StartDate").val();
        endDate = $("#EndDate").val();

        window.location = "/Summary/MgrdaycalExcel?startDate=" + startDate + "&endDate=" + endDate;
    }
</script>

<%--<script type="text/javascript">
    function printItem() {
        alert("Report");
        window.open("/Configuration/Managerdayrpt");
    }

</script> --%>
<script type="text/javascript">

    $(document).ready(function () {

        $('#RecordsContainer').jtable({
            paging: true,
            pageSize: 20,
            sorting: false,
            title: 'Managers Attendance Summary',
            defaultSorting: 'Name ASC',
            actions: {
                listAction: '<%=Url.Content("~/Summary/ManagerdaycalList") %>'
            },
            fields: {
                EMPID: {
                    title: 'EMP ID',
                    width: '4%'
                },
                ENAME: {
                    title: 'EMP Name',
                    width: '15%'
                },
                Designation: {
                    title: 'Designation',
                    width: '10%'
                },
                SECTION: {
                    title: 'Department',
                    width: '9%'
                },
                JDate: {
                    title: 'Join Date',
                    width: '12%'
                },
                Status: {
                    title: 'Status',
                    width: '5%'
                },
                TTDay: {
                    title: 'Total Days',
                    width: '6%'
                },
                Holiday: {
                    title: 'HoliDay',
                    width: '4%'
                },
                Present: {
                    title: 'Present',
                    width: '5%'
                },
                Absent: {
                    title: 'Absent',
                    width: '3%'
                },
                CL: {
                    title: 'CL',
                    width: '2%'
                },
                SL: {
                    title: 'SL',
                    width: '2%'
                },
                ML: {
                    title: 'ML',
                    width: '2%'
                },
                EL: {
                    title: 'EL',
                    width: '2%'
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
