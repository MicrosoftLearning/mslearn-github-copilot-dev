@echo off
echo Building and running E-Commerce Order Processing System...
echo.

dotnet build ECommerceOrderProcessing.sln
if %ERRORLEVEL% neq 0 (
    echo Build failed! Please check for errors.
    pause
    exit /b 1
)

echo.
echo Build successful! Running application...
echo.

cd src\ECommerce.Console
dotnet run

echo.
echo Application completed. Check order_audit_log.txt for detailed logs.
pause
