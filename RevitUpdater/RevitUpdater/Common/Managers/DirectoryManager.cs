using Serilog;

using System;
using System.IO;
using System.Reflection;

using RevitUpdater.Common.LogBase;
using RevitUpdater.Common.UpdaterBase;

namespace RevitUpdater.Common.Managers
{
    public class DirectoryManager
    {
        #region GetDllParentDirectoryPath

        /// <summary>
        /// DLL 파일의 부모 폴더 경로 구하기 
        /// </summary>
        public static string GetDllParentDirectoryPath(string pAssemblyFilePath)
        {
            string parentDirPath = string.Empty;                 // DLL 파일의 부모 폴더 경로
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // JSON 파일 "Updater_Parameters.json" 파일 경로 설정하기 (2024.04.03 jbh)
                // 주의사항 - 추후 해당 파일 경로를 NSIS 파일 셋업 파일 설치 완료 후 해당 파일이 설치된 경로에 존재하는
                //            json 파일에 접근해서 해당 json 데이터를 가져와야 함.
                // 파일 경로 예시 - "C:\Program Files\ImagineBuilder\HTSBIM_2019\Json" 폴더 하단에 존재하는 json 파일 "Updater_Parameters.json" 
                // 참고 URL   - https://rocabilly.tistory.com/114 
                // 참고 2 URL - https://mirwebma.tistory.com/132

                // TODO : 프로젝트 "HTSBIM2019" 로 생성된 "HTSBIM2019.dll" dll 파일 경로 찾기 (2024.04.03 jbh)
                // 참고 URL - https://docko.tistory.com/604
                // string testJsonPath = new ParamsManager().GetType().Assembly.Location;

                parentDirPath = pAssemblyFilePath;

                // 2. DLL 파일의 상위 디렉토리(폴더 - HTSBIM2019) 가져오기 
                // Path.GetDirectoryName 참고 URL - https://afsdzvcx123.tistory.com/entry/C-%EB%AC%B8%EB%B2%95-%ED%8C%8C%EC%9D%BC-%EA%B2%BD%EB%A1%9C%EC%97%90%EC%84%9C-%EB%94%94%EB%A0%89%ED%86%A0%EB%A6%AC-%EA%B2%BD%EB%A1%9C-%EA%B0%80%EC%A0%B8%EC%98%A4%EA%B8%B0
                // Directory.GetParent 참고 URL - https://chat.openai.com/c/0ed1e911-9425-4b35-9008-8e4a0a4493db
                while (true)
                {
                    // parentDirName = Path.GetDirectoryName(dllPath);
                    // dllPath = parentDirName;
                    DirectoryInfo parentDirectory = Directory.GetParent(parentDirPath);
                    parentDirPath = parentDirectory.FullName;

                    if (parentDirectory.Name.Equals(UpdaterHelper.AssemblyName)) break;
                }

                return parentDirPath;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion GetDllParentDirectoryPath

        #region Sample

        #endregion Sample
    }
}
