using System.Media;

namespace RevitUpdater.Models.UpdaterBase
{
    // TODO : Wav 음성 메시지 파일 경로에 접근해서 음성 메시지 소리 재생하는 SoundPlayer 클래스 프로퍼티 "WavSound" 구현 
    // 참고 URL - http://www.acronet.kr/index.php?mid=python&document_srl=27065
    // 참고 2 URL - https://learn.microsoft.com/ko-kr/dotnet/desktop/winforms/controls/how-to-play-a-sound-from-a-windows-form?view=netframeworkdesktop-4.8
    public class UpdaterInfo
    {
        /// <summary>
        /// 음성 파일(확장자 - .Wav) 
        /// </summary>
        public SoundPlayer WavSound { get; set; } 
         
        public UpdaterInfo(string pWavFilePath)
        {
            WavSound = new SoundPlayer(pWavFilePath);
        }
    }
}
