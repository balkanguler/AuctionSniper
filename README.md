# AuctionSniper
C# translation of the example source code in "Growing Object-Oriented Software, Guided by Tests"

Balkan Güler
balkanguler@gmail.com


1. Instead of Smack, I used agsXMPP SDK (http://www.ag-software.net/agsxmpp-sdk/) for XMPP client. 
I could not see chat implementation in agsXMPP, so I've written a simple chat layer over the agsXMPP SDK in a seperate project.

2. Instead of Window Licker, I used  TestStack (https://github.com/TestStack/White) automation of Windows Forms. 
I run application in a seperate process, because running tests and windows forms application in same process caused some unexpected behaviours. (For instance TestStack discovered grids as Panel instead of Table when run in same process)

3. I used NUnit for testing framework and NSubstitute for mocking.

4. I used OpenFire as XMPP server same as in the book. So you should install it, create user accounts as described in the book and write the server name in ApplicationRunner.XMPP_HOSTNAME field in the test project
