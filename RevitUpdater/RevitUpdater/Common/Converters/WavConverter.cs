using System.ComponentModel;


namespace RevitUpdater.Common.Converters
{
    
    #region Wav 유형
    public enum EnumWavType : int
    {
        [Description("-None-")]
        None = 0,
        [Description("MEP Updater 등록 완료.")]
        _1_MEP_Updater_Register_Start,
        [Description("MEP Updater 해제 완료.")]
        _2_MEP_Updater_Remove_Finished,
        [Description("MEP 업데이터 이미 해제 완료되었습니다.")]
        _2_MEP_Updater_Already_Finished,
    }
    #endregion Wav 유형

    public class WavConverter
    {

    }
}
