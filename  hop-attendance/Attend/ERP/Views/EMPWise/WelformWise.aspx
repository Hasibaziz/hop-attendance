﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<ERP.Domain.Model.WelformEntity>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    WelformWise
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="mp_left_menu">
        <% Html.RenderPartial("EMPWiseLeft"); %>
    </div>
    <div class="mp_right_content">
            <div class="page_list_container">
            <div><%--<div> Welform ID Wise:  </div>--%>
               Start Date: <%: Html.TextBoxFor(m => m.StartDate, new { @readonly = "true", @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                End Date: <%: Html.TextBoxFor(m => m.EndDate, new {@readonly = "true",  @class = "datefield Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                Employee ID: <%: Html.DropDownListFor(m => m.EMPID, (List<SelectListItem>)ViewData["EMPID"],"Select ID", new {@class = "DropDown Control_Width_100" })%> &nbsp; &nbsp;&nbsp;
                <input type="button" value="Show" title="Save"  id="GetAttenList" /> &nbsp; &nbsp;&nbsp;
                <input type="button" value="Print Report" title="Print"   onclick="printItem()" />
               <%-- <input type="button" value="Import Excel" title="Excel"   onclick="printItemEx()" />--%>
                <input type="button" value="Import Excel" title="Print"   onclick="impexcel()" />
            </div>
            <div id="RecordsContainer">
            </div>
        </div>
    </div>

<script type="text/javascript">
    function printItem() {
        alert("Report");
        window.open("/EMPWise/WelformWiserpt");
    }

</script>
<script type="text/javascript">
    function printItemEx() {
        alert("Report");
        window.open("/EMPWise/WelformWiserptExcel");
    }

</script>

<script type="text/javascript">
    function impexcel() {
        EX1 = $("#StartDate").val();
        EX2 = $("#EndDate").val();
        EX3 = $("#EMPID").val();
        window.location = "/EMPWise/WelformWiseExcel?EX1=" + EX1 + "&EX2=" + EX2 + "&EX3=" + EX3;
    }
</script>
<script type="text/javascript">

    $(document).ready(function () {

        $('#RecordsContainer').jtable({
            paging: true,
            pageSize: 31,
            sorting: false,
            title: 'Welform ID Wise:',
            defaultSorting: 'Name ASC',
            actions: {
                listAction: '<%=Url.Content("~/EMPWise/WelformWiseList") %>'
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
                    width: '10%'
                },
                EName: {
                    title: 'EMP Name',
                    width: '20%'
                },
                Designation: {
                    title: 'Designation',
                    width: '15%'
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
                EndDate: $('#EndDate').val(),
                EMPID: $('#EMPID').val()
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
//            yearRange: '-100y:c+nn',
            yearRange: '1900:+0',
            maxDate: '1d'
        });
    });

</script>

</asp:Content>
