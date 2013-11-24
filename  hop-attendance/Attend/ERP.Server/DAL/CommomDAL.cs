using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ERP.Domain.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace ERP.Server.DAL
{
    public class CommomDAL
    {
        public DataTable GetSaleCarDetailsInfo(object param)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "SELECT A.ID, A.HaatDate, A.SellerID, Byr.BuyerID, A.SoldDate, A.DeliveryDate, A.DeliveryTime, A.CarPrice, A.Advance,A.MRNo, " +
            "A.PaymentType, A.ChequeNo, A.Dues, A.ComPercen, A.TotalCom, A.StaffID, A.CarLocation, A.Notes,A.EntryDate, A.ModifyDate, A.UserID,CarDet.RegNo, " +
            "Br.Name+' '+Car.Name+' '+ CarDet.ManufYear +' '+ CarDet.RegYear CarDetails, " +
            "Br.Name BrName,Car.Name CarName,CarDet.ManufYear Model,CarDet.RegYear RegYear, " +
            "SLR.Name+' '+SLR.HomeAddress+' '+SLR.ContactNo +' '+SLR.Email SellerDetails, " +
            "Byr.Name+' '+Byr.HomeAddress+' '+Byr.ContactNo +' '+Byr.Email BuyerDetails, " +
            "Byr.Name+' '+Byr.ContactNo BuyerDetails1, " +
            "SLR.Name+' '+SLR.ContactNo SellerDetails1, " +
            "Byr.Name BuyerName,Byr.ContactNo BuyerContact, " +
            "SLR.Name SlrName,SLR.ContactNo slrContact, " +
            "CarDet.RegNo+' '+Car.Name DelvRegNo," +
            "case when A.DeliveryStatus=0 then 'Pending' else 'Delivered' end as DeliveryStatys,A.DeliveryStatus,DeliveryNotes,Ca.BRTA,Ca.SL FitnessBRTA,Ca.PostOffCurrent,Ca.PostOffPrev, " +
            "st.Name SoldBy,A.CarPrice,A.Advance,cast(A.Advance as decimal)+cast(isnull(RAdv.TotalReAdvance,0) as decimal) TotalBuyerPayment," +
            "cast(A.CarPrice as decimal) -(cast(A.Advance as decimal)+cast(isnull(RAdv.TotalReAdvance,0) as decimal)) TotalBuyerDues," +
            "isnull(SDed.TotalDeduct,0) TotalDeduct," +
            "isnull(RAdv.TotalReAdvance,0) TotalReAdvance," +
            "cast(A.CarPrice as decimal)-(cast(isnull(SDed.TotalDeduct,0) as decimal)+cast(isnull(SDep.TotalCom,0) as decimal)+cast(isnull(SPay.TotalSelerPayment,0) as decimal)) TotalDeposit," +
            "isnull(SPay.TotalSelerPayment,0) TotalSelerPayment   " +
            "FROM " +
            "ADM_HaatEntry B,ADM_Seller SLR,ADM_SellerCarDetails CarDet, " +
            "ADM_CarBrand Br,ADM_CarCatagory Cat,ADM_CarName Car, " +
            "ADM_CarSell A left outer join ADM_Buyer Byr on A.ID=Byr.SellID  left outer join ADM_SoldCarAdditinalInfo Ca on A.ID=Ca.BuyerID " +
            "left outer join (select SellID,sum(Amount) TotalReAdvance from ADM_ReAdvance group by SellID) RAdv on A.ID=RAdv.SellID " +
            "left outer join (select SellID,sum(Amount) TotalSelerPayment from ADM_SellerDeduct where perticulars='SELLER PAYMENT' group by SellID) SPay on A.ID=SPay.SellID " +
            "left outer join (select SellID,sum(Amount) TotalDeduct from ADM_SellerDeduct where perticulars!='COMISSION' and perticulars!='DEPOSIT' and perticulars!='SELLER PAYMENT' group by SellID) SDed on A.ID=SDed.SellID " +
            "left outer join (select SellID,sum(Amount) TotalCom from ADM_SellerDeduct where perticulars='COMISSION' group by SellID) SDep on A.ID=SDep.SellID " +
            "left outer join ADM_Staff st on st.StaffID=A.StaffID " +
            "where " +
            "B.ID=A.SellerID and B.SellerIDMain=SLR.ID and CarDet.SellerID=B.ID " +
            "and CarDet.CarNameID=Car.ID and Car.CatagoryID=Cat.ID and Cat.BrandID=Br.ID  ";

            CommonEntity obj = (CommonEntity)param;

            if (obj.StartDate != null)
                sql = sql + " and convert(datetime,A.HaatDate,103)>=convert(datetime,'" + obj.StartDate + "',103) and convert(datetime,A.HaatDate,103)<=convert(datetime,'" + obj.EndDate + "',103)";
            else
            {
                if (obj.HaatDate != null)
                    sql = sql + " and A.HaatDate='" + obj.HaatDate + "'";
            }

            //if (obj.Deliverydate != null)
            //{
            //    if (obj.Deliverydate == "1")
            //        sql = sql + " and isnull(SDed.TotalDeduct,0)>0";
            //    else
            //        sql = sql + " and isnull(SDed.TotalDeduct,0)=0";
            //}
            //if (obj.Deliverytime != null)
            //{
            //    if (obj.Deliverytime == "1")
            //        sql = sql + " and isnull(SDep.TotalDeposit,0)>0";
            //    else
            //        sql = sql + " and isnull(SDep.TotalDeposit,0)=0";
            //}
            //if (obj.Deliverystatus != null)
            //    sql = sql + " and A.DeliveryStatus=" + obj.Deliverystatus + " ";
            //if (obj.Sellerid != null)
            //    sql = sql + " and A.SellerID='" + obj.Sellerid + "'";
            //if (obj.Buyerid != null)
            //    sql = sql + " and Br.ID=" + obj.Buyerid + "";
            //if (obj.Chequeno != null)
            //    sql = sql + " and Cat.ID='" + obj.Chequeno + "'";
            //if (obj.Carprice != null)
            //    sql = sql + " and Car.ID=" + obj.Carprice + "";
            //if (obj.MrNo != null)
            //    sql = sql + " and CarDet.RegNo like '%" + obj.MrNo + "%'";



            sql = sql + " order by Byr.BuyerID ";

            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }
    }
}
