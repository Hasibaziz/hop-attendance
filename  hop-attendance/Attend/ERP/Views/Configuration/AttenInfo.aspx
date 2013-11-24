<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<ERP.Domain.Model.AttendanceEntity>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AttenInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div class="mp_left_menu">
        <% Html.RenderPartial("LeftMenu"); %>
    </div>
    <div class="mp_right_content">
            <div class="page_list_container">
            <div><%--<div> North Tower Management </div>--%>
                Start Date: <%: Html.TextBoxFor(m => m.StartDate, new { @readonly = "true", @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                End Date: <%: Html.TextBoxFor(m => m.EndDate, new {@readonly = "true",  @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                <input type="button" value="Show" title="Show"  id="GetAttenList" /> &nbsp; &nbsp;&nbsp;
                <input type="button" value="Print Report" title="Print"   onclick="printItem()" />
               <%-- <input type="button" value="Save As Excel" title="Excel"   onclick="excel_btn()" />
                <input type="button" value="Save All" title="Save All"   onclick="BTNALL()" />--%>
                <input type="button" value="Excel" title="Excel Report"   onclick="impexcel()" />
                <%-- <input type="button" value="Report All" title="Print"   onclick="printItemall()" />--%>
               <%-- <input type="button" value="Print Excel" title="Excel"   onclick="excel_btn()" />--%>
            </div>
            <div id="RecordsContainer">
            </div>
        </div>
    </div>


<script type="text/javascript">
    function impexcel() {
        EX1 = $("#StartDate").val();
        EX2 = $("#EndDate").val();
        window.location = "/Configuration/ExcelReport?EX1=" + EX1 + "&EX2=" + EX2;
    }
</script>

<script type="text/javascript">
function BTNALL() {
   p1= $("#StartDate").val();
   p2= $("#EndDate").val();

   window.location = "/Configuration/-NorthTowerrptall?p1=" + p1 + "&p2=" + p2;
}
</script>


<script type="text/javascript">
    function excel_btn() {
        alert("Report");
        window.open("/Configuration/-ReportViewExcel");
    }

</script>
<%--<script type="text/javascript">
    function printItemall() {
        alert("Report");
        window.open("/Configuration/NorthTowerrptall");
    }

</script>--%>


<script type="text/javascript">
        function printItem() {
            alert("Report");
            window.open("/Configuration/ReportView01");
        }

</script>
<script type="text/javascript">
       
        $(document).ready(function () {

            $('#RecordsContainer').jtable({
                paging: true,
                pageSize: 20,
                sorting: false,
                title: 'North Tower Management',
                defaultSorting: 'Name ASC',
                actions: {
                    listAction: '<%=Url.Content("~/Configuration/AttenInfoList") %>'
                },
                fields: {
//                    Id: {
//                        key: true,
//                        create: false,
//                        edit: false,
//                        list: false
//                    },
                    SL:{
                      title:'SL',
                      width:'2%'
                    },
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
