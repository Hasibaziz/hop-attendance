using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Server.BLL;
namespace ERP.Structure.TaskManager
{
    internal class ErpTaskManager
    {
        public object Execute(string methodName, object param)
        {
            switch (methodName)
            {
                case ERPTask.MG_GetSaleCarDetailsInfo:
                    CommonBLL commonBLL = null;
                    commonBLL = new CommonBLL();
                    return commonBLL.GetSaleCarDetailsInfo(param);
                    break;

                #region Auto Generated - TR_ServiceMaster
                case ERPTask.AG_SaveTrServicemasterInfo:
                    TrServicemasterBLL trServicemasterBLL = null;
                    trServicemasterBLL = new TrServicemasterBLL();
                    return trServicemasterBLL.SaveTrServicemasterInfo(param);
                    break;
                case ERPTask.AG_UpdateTrServicemasterInfo:
                    trServicemasterBLL = new TrServicemasterBLL();
                    return trServicemasterBLL.UpdateTrServicemasterInfo(param);
                    break;
                case ERPTask.AG_DeleteTrServicemasterInfoById:
                    trServicemasterBLL = new TrServicemasterBLL();
                    return trServicemasterBLL.DeleteTrServicemasterInfoById(param);
                    break;
                case ERPTask.AG_GetAllTrServicemasterRecord:
                    trServicemasterBLL = new TrServicemasterBLL();
                    return trServicemasterBLL.GetAllTrServicemasterRecord(param);
                    break;
                case ERPTask.AG_GetSingleTrServicemasterRecordById:
                    trServicemasterBLL = new TrServicemasterBLL();
                    return trServicemasterBLL.GetSingleTrServicemasterRecordById(param);
                    break;
                #endregion

                #region Auto Generated - TR_ServiceDetails
                case ERPTask.AG_SaveTrServicedetailsInfo:
                    TrServicedetailsBLL trServicedetailsBLL = null;
                    trServicedetailsBLL = new TrServicedetailsBLL();
                    return trServicedetailsBLL.SaveTrServicedetailsInfo(param);
                    break;
                case ERPTask.AG_UpdateTrServicedetailsInfo:
                    trServicedetailsBLL = new TrServicedetailsBLL();
                    return trServicedetailsBLL.UpdateTrServicedetailsInfo(param);
                    break;
                case ERPTask.AG_DeleteTrServicedetailsInfoById:
                    trServicedetailsBLL = new TrServicedetailsBLL();
                    return trServicedetailsBLL.DeleteTrServicedetailsInfoById(param);
                    break;
                case ERPTask.AG_GetAllTrServicedetailsRecord:
                    trServicedetailsBLL = new TrServicedetailsBLL();
                    return trServicedetailsBLL.GetAllTrServicedetailsRecord(param);
                    break;
                case ERPTask.AG_GetSingleTrServicedetailsRecordById:
                    trServicedetailsBLL = new TrServicedetailsBLL();
                    return trServicedetailsBLL.GetSingleTrServicedetailsRecordById(param);
                    break;
                #endregion



                #region Auto Generated - AttendanceInfo
                case ERPTask.AG_GetAttenInfoRecord:
                    AttendanceBLL trAttendanceBLL = null;
                    trAttendanceBLL = new AttendanceBLL();
                    return trAttendanceBLL.GetAttendanceInfoRecord(param);
                    break;
               
                #endregion

                #region Auto Generated - WelformInfo
                case ERPTask.AG_GetWelformRecord:
                    WelformattenBLL trWelformattenBLL = null;
                    trWelformattenBLL = new WelformattenBLL();
                    return trWelformattenBLL.GetWelformInfoRecord(param);
                    break;
                case ERPTask.AG_GetWelformWiseRecord:
                    trWelformattenBLL = new WelformattenBLL();
                    return trWelformattenBLL.GetWelformWiseRecord(param);
                    break;
                case ERPTask.AG_GetAllWelformWiseRecord:
                    trWelformattenBLL = new WelformattenBLL();
                    return trWelformattenBLL.GetAllWelformWiseRecord(param);
                    break;

                #endregion

                #region Auto Generated - EmployeeWise
                case ERPTask.AG_GetEmployeewiseRecord:
                    EmployeewiseBLL trEmployeewiseBLL = null;
                    trEmployeewiseBLL = new EmployeewiseBLL();
                    return trEmployeewiseBLL.GetEmployeewiseRecord(param);
                    break;
                case ERPTask.AG_GetAllEmployeewiseRecord:
                    trEmployeewiseBLL = new EmployeewiseBLL();
                    return trEmployeewiseBLL.GetAllEmployeewiseRecord(param);
                    break;

                #endregion

                #region Auto Generated - ManagerInfo
                case ERPTask.AG_GetManagerRecord:
                    ManagerInfoBLL trManagerInfoBLL = null;
                    trManagerInfoBLL = new ManagerInfoBLL();
                    return trManagerInfoBLL.GetManagerInfoRecord(param);
                    break;
                case ERPTask.AG_GetAllManagerInfoRecord:
                    trManagerInfoBLL = new ManagerInfoBLL();
                    return trManagerInfoBLL.GetAllManagerInfoRecord(param);
                    break;
                case ERPTask.AG_GetAllManagerRecord:
                    trManagerInfoBLL = new ManagerInfoBLL();
                    return trManagerInfoBLL.GetAllManagerRecord(param);
                    break;
                case ERPTask.AG_GetManagerReportView:
                    trManagerInfoBLL = new ManagerInfoBLL();
                    return trManagerInfoBLL.GetManagerReportView(param);
                    break;
                #endregion

                #region Auto Generated - ApprealInfo
                case ERPTask.AG_GetApprealInfoRecord:
                    ApprealInfoBLL trApprealInfoBLL = null;
                    trApprealInfoBLL = new ApprealInfoBLL();
                    return trApprealInfoBLL.GetApprealInfoRecord(param);
                    break;
                case ERPTask.AG_GetApprealEMPWiseRecord:
                    trApprealInfoBLL = new ApprealInfoBLL();
                    return trApprealInfoBLL.GetApprealEMPWiseRecord(param);
                    break;
                case ERPTask.AG_GetAllAppearlRecord:
                    trApprealInfoBLL = new ApprealInfoBLL();
                    return trApprealInfoBLL.GetAllAppearlRecord(param);
                    break;
                #endregion

                #region Auto Generated - BDEmployee
                case ERPTask.AG_GetBDEmployeeRecord:
                    BDEmployeeBLL BDEmployeeBLL = null;
                    BDEmployeeBLL = new BDEmployeeBLL();
                    return BDEmployeeBLL.GetBDEmployeeRecord(param);
                    break;
                case ERPTask.AG_GetBDEmployeewiseRecord:
                    BDEmployeeBLL = new BDEmployeeBLL();
                    return BDEmployeeBLL.GetBDEmployeewiseRecord(param);
                    break;
                case ERPTask.AG_GetAllBDEmployeewiseRecord:
                    BDEmployeeBLL = new BDEmployeeBLL();
                    return BDEmployeeBLL.GetAllBDEmployeewiseRecord(param);
                    break;
                #endregion

                #region Auto Generated - Northtowerdaycal
                case ERPTask.AG_GetNorthtowerdaycalRecord:
                    NorthtowerdaycalBLL NorthtowerBLL = null;
                    NorthtowerBLL = new NorthtowerdaycalBLL();
                    return NorthtowerBLL.GetNorthtowerdaycalRecord(param);
                    break;
                #endregion

                #region Auto Generated - Welformdaycal
                case ERPTask.AG_GetWelformdaycalRecord:
                    WelformdaycalBLL WelformdBLL = null;
                    WelformdBLL = new WelformdaycalBLL();
                    return WelformdBLL.GetWelformdaycalRecord(param);
                    break;
                #endregion

                #region Auto Generated - Managerdaycal
                case ERPTask.AG_GetManagerdaycalRecord:
                    ManagerdaycalBLL ManagerdayBLL = null;
                    ManagerdayBLL = new ManagerdaycalBLL();
                    return ManagerdayBLL.GetManagerdaycalRecord(param);
                    break;
                #endregion

                #region Auto Generated - Appearldaycal
                case ERPTask.AG_GetAppearldaycalRecord:
                    AppearldaycalBLL AppearldayBLL = null;
                    AppearldayBLL = new AppearldaycalBLL();
                    return AppearldayBLL.GetAppearldaycalRecord(param);
                    break;
                #endregion

                #region Auto Generated - BDEmployeedaycal
                case ERPTask.AG_GetBDEmployeedaycalRecord:
                    BDEmployeedaycalBLL BDdayBLL = null;
                    BDdayBLL = new BDEmployeedaycalBLL();
                    return BDdayBLL.GetBDEmployeeRecord(param);
                    break;
                #endregion


                #region Auto Generated - GetDeptWiseRecord
                case ERPTask.AG_GetNTDeptRecord:
                    DeptwiseallBLL NTBLL = null;
                    NTBLL = new DeptwiseallBLL();
                    return NTBLL.GetNTDeptRecord(param);
                    break;
                case ERPTask.AG_GetAllDeptRecord:
                    NTBLL = new DeptwiseallBLL();
                    return NTBLL.GetAllDeptRecord(param);
                    break;
                case ERPTask.AG_GetWFDeptRecord:
                    NTBLL = new DeptwiseallBLL();
                    return NTBLL.GetWFDeptRecord(param);
                    break;
                case ERPTask.AG_GetAPPDeptRecord:
                    NTBLL = new DeptwiseallBLL();
                    return NTBLL.GetAPPDeptRecord(param);
                    break;
                case ERPTask.AG_GetMGRDeptRecord:
                    NTBLL = new DeptwiseallBLL();
                    return NTBLL.GetMGRDeptRecord(param);
                    break;
                case ERPTask.AG_GetBDDeptRecord:
                    NTBLL = new DeptwiseallBLL();
                    return NTBLL.GetBDDeptRecord(param);
                    break;
                #endregion



                default:
                    break;
            }
            return null;
        }
        public object Execute(string methodName)
        {
            throw new NotImplementedException();
        }
    }
}
