# wintree

## Overview

wintree is an explorer-style launcher tool for windows. It uses an XML document to store items and has a built in time tracking feature. It is written in C# and WPF.

![wintree](https://github.com/dmaccormac/wintree/blob/main/img/cap1.png)

## Setup

- Download the latest release [here](https://github.com/dmaccormac/wintree/releases/tag/wintree)
- Unzip `wintree.zip` archive and run `setup.exe`

## Usage

Open wintree from the Start Menu. On first run, the sample XML file will be loaded.

### Menu Items

To bring up the app menu, right click on the tree.

![wintree](https://github.com/dmaccormac/wintree/blob/main/img/cap2.png)

| Menu Item     | Descripton                           |
| ------------- | ------------------------------------ |
| Launch        | Open selected item                   |
| Open XML      | Open an XML tree file                |
| Edit XML      | Edit the current XML tree file       |
| Refersh       | Reload tree items from XML file      |
| View Log      | View the log file                    |
| Always on Top | Keep application above other windows |
| About         | Display application info             |

### Time tracking

Each time an item in the tree is opened or closed, it is recorded in the log file along with a timestamp.

## Configuration

### XML tree file

The sample file _wintree.xml_ is shown below.

#### Sample XML

    <?xml version="1.0" encoding="utf-8" ?>
    <Tree Name="MyTree">
        <Group Name="Workstations" Icon="icons\folder.ico">
            <Item Name="Alice" Command="mstsc.exe" Parameters="/V:alice-pc" Icon="icons\computer.ico"/>
            <Item Name="Bob" Command="mstsc.exe" Parameters="/V:bob-pc" Icon="icons\computer.ico"/>
        </Group>

        <Group Name="Servers" Icon="icons\folder.ico">
            <Item Name="Contoso DC1"  Command="mstsc.exe" Parameters="/V:dc1.corp.contoso.com" Icon="icons\server.ico"/>
            <Item Name="Contoso FS1"  Command="mstsc.exe" Parameters="/V:fs1.corp.contoso.com"  Icon="icons\server.ico"/>
        </Group>
    </Tree>

wintree uses three XML tags to store the tree information: `Tree`, `Group` and `Tag`

An overview of the tags is shown below. Optional attributes are denoted with ?

| Tag   | Description                                               | Attributes                       |
| ----- | --------------------------------------------------------- | -------------------------------- |
| Tree  | Top level tag which wraps the entire tree structure       | Name                             |
| Group | A collection of items displayed a folder in the tree      | Name, Icon?                      |
| Item  | Used to specify the command and paramters of the launcher | Name, Command, Parameters, Icon? |
