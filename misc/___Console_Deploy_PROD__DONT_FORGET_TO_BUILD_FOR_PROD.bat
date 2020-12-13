del \\usidcwvweb11\E$\ConsoleApps\Level3.AddressManagement.Console\*.* /Q

set src=C:\Sandbox\Level3.AddressManagement\Level3.AddressManagement.Console\bin\ProdWebServerCluster
set dst=\\usidcwvweb11\E$\ConsoleApps\Level3.AddressManagement.Console

xcopy %src% %dst% /C /S /D /Y /I
