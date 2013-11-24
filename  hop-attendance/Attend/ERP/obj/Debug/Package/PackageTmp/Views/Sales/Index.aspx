<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<ERP.Models.SalesCarList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mp_left_menu">
        <% Html.RenderPartial("LeftMenu"); %>
    </div>
    <div class="mp_right_content">
        <div class="page_list_container">
            <div style="float: left; width: 100%">
                Start Date:<%: Html.TextBoxFor(m => m.StartDate, new { @readonly = "true", @class = "datefield Control_Width_100" })%>
                End Date:<%: Html.TextBoxFor(m => m.EndDate, new { @readonly = "true", @class = "datefield Control_Width_100" })%>
                <input type="button" value="Get Sales List" title="Save" id="btnGetCarList" />
            </div>
            <div style="float: left; width: 100%;">
                <div id="RecordsContainer">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {

            $('#RecordsContainer').jtable({
                paging: true,
                pageSize: 10,
                sorting: false,
                defaultSorting: 'Name ASC',
                actions: {
                    listAction: '<%=Url.Content("~/Sales/jGetSalesList") %>',
                },
                fields: {
                    Id: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    HaatDate: {
                        title: 'HaatDate',
                        width: '10%'
                    },
                    Regno: {
                        title: 'Reg No',
                        width: '10%'
                    },
                    CarReview: {
                        title: 'Car Details',
                        width: '20%'
                    },
                    BuyerID: {
                        title: 'Buyer ID',
                        width: '10%',
                        display: function (data) {
                            return '<a href="/ACC/Report/VoucherReport/' + data.record.ID + '" target="_blank">' + data.record.BuyerID + '</a>';
                        }
                    },
                    BuyerName: {
                        title: 'Buyer Details',
                        width: '20%'
                    },
                    TotalDeduct: {
                        title: 'Total Deduct',
                        width: '10%'
                    },
                    TotalDeposit: {
                        title: 'Total Deposit',
                        width: '10%'
                    },
                    TotalBuyerPayment: {
                        title: 'Payment',
                        width: '10%'
                    },
                    TotalBuyerDues: {
                        title: 'Dues',
                        width: '10%'
                    }
                }
            });
             $('#btnGetCarList').click(function (e) {
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
