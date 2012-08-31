aws-glacier-cmd
===============

Quick and easy commandline tool for using Amazon Glacier

NOTE: this project was built using MonoDevelop.  The AWSSDK.dll was also built in MonoDevelop.  I have no idea if it will work with Visual Studio.

Getting Started
===============

    sudo apt-get install monodevelop mono-complete
    monodevelop

Open Glacier.sln

Build release

    cd Glacier/bin/Release/

Edit Glacier.exe.config and put in your AWSAccessKey and AWSSecretKey.

Create a Vault in Glacier.

Now you are set!

      # upload a file
      mono Glacier.exe -v <vaultname> <filename>
      # help text
      mono Glacier.exe --help

 
