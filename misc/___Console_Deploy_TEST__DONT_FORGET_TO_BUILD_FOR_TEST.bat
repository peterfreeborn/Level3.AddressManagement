del \\usidcwvweb13\D$\ConsoleApps\Level3.AddressManagement.Console\*.* /Q

set src=C:\Sandbox\Level3.AddressManagement\Level3.AddressManagement.Console\bin\Test
set dst=\\usidcwvweb13\D$\ConsoleApps\Level3.AddressManagement.Console

xcopy %src% %dst% /C /S /D /Y /I
