## **Claim**

1.  This program is licensed under MIT License. It is a free and open source software, so the author does not assume any direct or indirect responsibility, please evaluate it yourself and use it after you can accept it.
2.  The fonts used in the UI graphic of this program are "Zen Antique" and "DSEG Font Family (v0.46)" , both licensed under SIL Open Font License v1.1.
3.  Special thanks to Keepass for providing the security concept, and to ChatGPT for providing some of the code.

## Who is this program suitable for?

*   Want to use the account password management program to manage some accounts and confidential information.
*   Don't want to pay for it.
*   Can't trust any cloud-based password service, Internet-connected manager, or non-open source manager.
*   Worried that complex authentication mechanisms may go wrong, additional devices may fail.
*   Don't want to install, want to be able to control and back up all the data myself.
*   Love simple things, old-fashioned technology, traditional technology, classic technology.
*   Love Evangelion (with its style).
*   Afraid that the popular manager has become the target of hackers, and want to use the unpopular manager.
*   The program is so small that you can even put it on a floppy disk if you want.

## Operation Guide

### Getting Started

*   Put the program in a prepared folder.
*   Open the program and you will see an interface for entering a password. Think of a secure and memorable password, enter it twice, and click "Confirm" to get started.
*   The password you enter here will become the encrypted "key seed" and directly correspond to a unique "Catalog", which can manage multiple accounts.
*   To open this catalog in the future, simply enter this password. Just type it in once.
*   Similarly, if you want to add different groups of catalogs, just enter different passwords.
*   It is also possible to choose a file instead of a password (use Open file). The file name is not important, but the contents of the file cannot be changed at all.

### Basic Operations

*   Once you get into it, there is a list on the left side. At the top is "New Account", click it to add an account.
*   Then enter your account information into the right column. If you don’t understand the meaning, you can let the mouse stay on it, and there will be an explanation.
*   You don't need to fill in all the fields except the title. When you are done, click "Save" to finish adding your account and it will appear in the list.
*   If you want to reorder, you can use the "Move Up/Down" button, and if you want to delete, click "Delete".
*   To switch to another catalog, use the "Logout" button. To exit, click the "Finish" button.

### Advanced Operations

*   "Launch" button: Add an executable file or a internet URL. Click button will open it for you.
*   "View" button: Allows you to view hidden information to prevent anyone from peeking.
*   "Copy" button: Copies the selected information to the clipboard.
*   "Transfer" button: You can transfer this account data to another catalog.
*   You can set the name of the catalog in "Catalog Setting and Management".

### More Advanced Operations

*   You can set features like "Auto Hotkey", "Automatic Logout When Idle", and "Transfer or Delete All Catalogs" in "Catalog Setting and Management".
*   If you have problems with using hotkeys for "Copy & Paste" auto input, you can use the "Send key" mode to override for separately accounts. (v1.2 or later)
*   If you rename the language file to Lang\_MOD.TXT, the interface will display in the corresponding language (Text message only; Graphic part will not change).
*   You can use the command line parameter "NONOTICE" to hide the NOTICE at the bottom of the window. (Please read it once before hiding.)
*   You can use the command line parameter "OPACITY,nn" to set the window opacity. (100 = opaque (default), and the initial version is fixed at 93.)

### Cryptocurrency related Operations

*   No validation is required when using BIP39 passphrase, as it is automatically verified.
*   Before save, certain errors in cryptocurrency addresses will be automatically detected. Currently, BTC (TRX, Doge, LTC), ETH and compatible coins are supported.

### Backup operation and [CSV Import/Export](CSV_EN.md)

*  Generally, simply copy the main program along with the generated folders and files. You can choose to compress, save to a USB drive, or upload to the cloud.
*  For CSV import/export, please read the document "[CSV Inport/Export](CSV_EN.md)".

### Enhanced security operations

*  On the the program start, there is a "Secure Desktop" option. After selecting it and restart, the secure desktop function can be enabled. This function can also be set as a shortcut with the parameter "SECUREDESKTOP".
*  If you select "RUN AS ADMIN" and then restart, you can raise the administrator privileges to make the program more secure.
*  You can set the salt by using the command-line parameter SALT,xx.. ( The login form will display "SALT".)

### Command line parameter list

1. "SECUREDESKTOP" to enable secure desktop.
2. "NONOTICE" turns off NOTICE under the login window.
3. "OPACITY,nn" sets the opacity of the window (100 = opaque (default)).
4. "SALT,xx..." Set salt, use any text except space and comma.

## **About Security Technology**

### **About the intrinsic safety of the program**

1.  Open source, licensed under MIT license.
2.  A standalone executable file, with no third-party library used (only Microsoft's official DPAPI library).

### **About Encryption and Storage (Core Security Technology)**

1.  The catalog password is hash 2 million times with SHA256, which is equivalent to increasing the password by 3.5 digits. (Note 1)
2.  The final archive is encrypted with AES256 CBC mode, PKCS7 padding, and a random IV (Note 2).
3.  Random data of varying lengths is added to the archive to obfuscate the real data and password length.
4.  Any file can be used as the password. The maximum size is 128MB.
5.  Automatic zero-fill rewrite function for files (when saving/deleting) (Note 3).
6.  You can use salt (optional) (Note 4).

### **About Program Security Technology (Note 5)**

1.  DPAPI is used to protect sensitive data during runtime.
2.  Memory leak cleaning (protects against approximately 95% of memory leaks).
3.  Secure Desktop mode is available, which is resistant to keyboard and screen recorders (optional).
4.  Clipboard monitoring and blocking technology is used (WM interception).
5.  Memory Page Lock is used to prevent sensitive information from being swapped to the swap file (Note 6).
6.  Windows executable security mitigation policies: ASLR/DEP/StrictHandle/ExtensionPoint/SignaturePolicy/ImageLoad/SideChannelIsolation.
7.  Hybrid hotkey automatic input mode.

### About Operating Safety Features

1.  Automatic logout function when idle.
2.  When "Copy & paste" into the password box, it will prompt you to clear the clipboard.

### Notes

1.  This is KDF, and is only effective against brute-force guessing attacks. To resist rainbow table attacks, salting is necessary.
2.  IV Random values are not from data hashing, but are re-encrypted only after data modification.
3.  Not guaranteed to work, especially on disks with special features such as compression enabled. In addition, the nature of SSDs also allows data to remain on the physical layer, and to be completely secure, full disk space rewriting (search for Wipe Free Space) is required.
4.  As this is an open-source program, a fixed salting method is meaningless and must be determined by the user. In addition, different salting methods cannot be transfer between catalogs, so please be aware of this.
5.  This type of security measure is to defend against hackers or Trojan invasion. It works better with administrator privileges. **But you should know that all this is just better than nothing and the most important thing is to ensure that your computer never be hacked.**
6.  Users need to enable the "Security Policy" in Windows.

## **Q&A**

*   **Q:** Why did you write this software?
*   **A:** Originally it was for my personal use, but later I decided to share it and see if I could make some friends. _**I have no friends, and I couldn't even find someone to help me test the program. My life is so lonely...**_ 😭
*   **Q:** Why didn't you use Argon2 or other anti-GPU hashing algorithms, but chose traditional SHA-256 for salting?
*   **A:** The simplest reason is that SHA-256 is a built-in function, so there's no need to deal with third-party libraries  or worry about whether it has been tested historically. And even if it's relatively anti-GPU, I don't believe it can resist ASIC.
*   **Q:** Will there be a version for mobile devices or cross-platform use?
*   **A:** I am not familiar with mobile app development and have no need for it, so there won't be one.
*   **Q:** The online virus scanning said there are viruses?
*   **A:** Because I used some security techniques that are less commonly used in ordinary software, being misjudged is inevitable. In any case, downloading from the correct website URL, checking the code, compiling it yourself, is the safest and most reliable.
*   **Q:** What should I do if I forget the catalog password / salt?
*   **A:** Well, that's a disaster. There's nothing I can do for you. So never forget the catalog password / salt.
*   **Q:** Can you add a certain feature or change a certain usage?
*   **A:** Unless the software has a very basic bug, this is probably it. If you want to add or change something, please do it yourself. This is open source.
*   **Q:** What kind of Japanese are you writing? It's just a bunch of nonsense.
*   **A:** Actually, with the help of various AI translation tools, it's not difficult to write normal and correct Japanese. The difficulty lies in matching the style of using kanji that is particularly favored in Evangelion. I have tried my best.
*   **Q:** Q: Why can salting only be done through command line parameters and not in the password input interface?
*   **A:** A: "Salting" can be thought of as an additional extension to the password. If you have to manually enter the salt each time, it's better to just make the password longer.
