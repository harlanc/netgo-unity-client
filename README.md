# netgo-unity-client

## Introduction

netgo-unity-client is a unity client SDK for [netgo](https://github.com/netgo-framework/netgo),it is easy to use and contains a demo on how to use it.

## Start the Demo

### Environment 

- Unity 2018.4.2f1
- MacOS Catalina
- Android Mobile Phone

### Build the APK for Android

##### Configure Network

Open the **DemoLobbyScene** and configure the [netgo server](https://github.com/netgo-framework/netgo) IP address and port.

![](http://qiniu.harlanc.vip/12.24.2019_8:03:50.png)

##### Switch Platform

Switch the Platform to Android.

![](http://qiniu.harlanc.vip/12.24.2019_7:46:29.png)

##### Build 

Click **Build** to generate the APK.

### Other Platforms

netgo-unity-client can also be run on MacOS/IOS/Windows.


### Run and Operate

##### Menu

You should install and run the APK on more than one clients.Look at the lobby scene menu:

![](http://qiniu.harlanc.vip/12.24.2019_9:41:23.png)

1. **Room Name :** Define a unique name for a Room.
2. **Room Capacity :** The count which one Room can hold.
3. **Join Or Create Room:**If the Room exists , then join the Room ,else create a new Room.
4. **Create Room:** Just create the Room.
5. **Join Room:** Just join the Room.

##### Communications

When a client first join a room, a cube will be instantiated(Here we call **Mine Cube**).You will be cloned(Here we call **Clone Cube**) on other client when other one join the same room.You can click the **Mine Cube** to change the colors,it can be synced by using **[RPC](https://github.com/netgo-framework/netgo-unity-client/tree/master/Assets/Netgo/Application/RPC)**:

![](http://qiniu.harlanc.vip/netgo-client-rpc.gif)

And we click **Move** button to let the two cubes move.We here use **[View Sync](https://github.com/netgo-framework/netgo-unity-client/tree/master/Assets/Netgo/Application/ViewSynchronizer)** to sync the positions and rotations for each other:

![](http://qiniu.harlanc.vip/netgo-client-viewsync3.gif)

When you click the **Clone Cube**,we here **[Custom Event](https://github.com/netgo-framework/netgo-unity-client/tree/master/Assets/Netgo/Application/CustomEvent)** to send a message to **Mine Cube**.

For details you can review the revelant souce codes.







