@echo off
cd "c:\Users\cahowd\MSLearn-GitHub-Copilot\mslearn-github-copilot-dev\DownloadableCodeProjects\standalone-lab-projects\implement-performance-profiling\ContosoOnlineStore"
echo Starting Contoso Online Store Application...
dotnet run > application_output.txt 2>&1
echo Application completed. Output saved to application_output.txt
type application_output.txt
