Vagrant.configure("2") do |config|
  # Ubuntu VM Configuration
  config.vm.define "ubuntu" do |ubuntu|
    configure_ubuntu(ubuntu)
  end

  # Windows VM Configuration
  config.vm.define "windows" do |windows|
    configure_windows(windows)
  end

  # macOS VM Configuration
  config.vm.define "macos" do |macos|
    configure_macos(macos)
  end
end

# Function to configure Ubuntu
def configure_ubuntu(vm)
  vm.vm.box = "bento/ubuntu-22.04"
  vm.vm.hostname = "VMUbuntu"
  vm.vm.network "forwarded_port", guest: 7258, host: 7258
  vm.vm.network "private_network", ip: "192.168.34.42"
  
  vm.vm.provider "virtualbox" do |vb|
    vb.name = "VMUbuntu"
    vb.gui = false
    vb.memory = "4024"
    vb.cpus = 8
  end

  vm.vm.synced_folder ".", "/home/vagrant/project"
  vm.ssh.insert_key = false

  # Provision Ubuntu VM
  vm.vm.provision "shell", inline: install_dotnet_on_ubuntu
end

# Function to configure Windows
def configure_windows(vm)
  vm.vm.box = "StefanScherer/windows_2019"
  vm.vm.communicator = "winrm"
  
  vm.vm.provider "virtualbox" do |vb|
    vb.name = "VM_Windows"
    vb.gui = true
    vb.memory = "8048"
    vb.cpus = 4
    vb.customize ["modifyvm", :id, "--vram", "128"]
    vb.customize ["modifyvm", :id, "--natdnshostresolver1", "on"]
    vb.customize ["modifyvm", :id, "--natdnsproxy1", "on"]
    vb.customize ["modifyvm", :id, "--clipboard", "bidirectional"]
  end

  # Configure WinRM
  vm.winrm.username = "user2024"
  vm.winrm.password = "user2024"
  vm.winrm.transport = :negotiate
  vm.winrm.basic_auth_only = false

  # Forwarded Ports
  vm.vm.network "forwarded_port", guest: 5000, host: 5100, auto_correct: true
  vm.vm.network "forwarded_port", guest: 4569, host: 4900, auto_correct: true
  vm.vm.network "forwarded_port", guest: 3005, host: 7546, auto_correct: true
  vm.vm.network "forwarded_port", guest: 5300, host: 4245, auto_correct: true

  # Provision Windows VM
  vm.vm.provision "shell", inline: install_dotnet_on_windows
end

# Function to configure macOS
def configure_macos(vm)
  vm.vm.box = "ramsey/macos-catalina"
  vm.vm.hostname = "VMMac"
  
  vm.vm.provider "virtualbox" do |vb|
    vb.name = "VMMac"
    vb.gui = true
    vb.memory = "8048"
    vb.cpus = 4
  end

  # Provision macOS VM
  vm.vm.provision "shell", inline: install_dotnet_on_macos
end

# Shell scripts for provisioning
def install_dotnet_on_ubuntu
  <<-SHELL
    # Remove existing .NET packages if they exist
    sudo apt remove -y 'dotnet*' 'aspnet*' 'netstandard*'

    # Configure apt preferences to ignore system .NET packages
    echo "Package: dotnet* aspnet* netstandard*" | sudo tee /etc/apt/preferences.d/dotnet
    echo "Pin: origin \"archive.ubuntu.com\"" | sudo tee -a /etc/apt/preferences.d/dotnet
    echo "Pin-Priority: -10" | sudo tee -a /etc/apt/preferences.d/dotnet
    echo "" | sudo tee -a /etc/apt/preferences.d/dotnet
    echo "Package: dotnet* aspnet* netstandard*" | sudo tee -a /etc/apt/preferences.d/dotnet
    echo "Pin: origin \"security.ubuntu.com\"" | sudo tee -a /etc/apt/preferences.d/dotnet
    echo "Pin-Priority: -10" | sudo tee -a /etc/apt/preferences.d/dotnet

    # Add Microsoft repository
    wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb

    # Update repositories
    sudo apt-get update

    # Install .NET SDK and other dependencies
    sudo apt-get install -y gpg curl wget apt-transport-https software-properties-common
    sudo apt-get install -y dotnet-sdk-6.0

    # Verify installation
    dotnet --info || { echo "Error installing .NET SDK"; exit 1; }

    # Add BaGet as a NuGet source
    dotnet nuget add source http://10.0.2.2:5000/v3/index.json -n "BaGet"
    
    # Install NPushkaruk tool
    dotnet tool install --global NPushkaruk --version 1.0.5 --add-source http://10.0.2.2:5000/v3/index.json

    # Add tools to PATH
    echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
    source ~/.bashrc

    # Verify NPushkaruk tool installation
    SGoNPushkaruklouh --help || { echo "The NPushkaruk tool was not installed correctly"; exit 1; }
  SHELL
end

def install_dotnet_on_windows
  <<-SHELL
    # Install Chocolatey
    Set-ExecutionPolicy Bypass -Scope Process -Force
    iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
    
    # Install .NET SDK using Chocolatey
    choco install dotnet-sdk -y --no-progress
    refreshenv
    
    # Add BaGet source for .NET
    dotnet nuget add source http://10.0.2.2:5000/v3/index.json -n "BaGet"
    
    # Install SGolouh tool
    dotnet tool install --global SGolouh --version 1.0.5 --add-source http://10.0.2.2:5000/v3/index.json
  SHELL
end

def install_dotnet_on_macos
  <<-SHELL
    # Install Homebrew
    /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
    
    # Install .NET SDK
    brew update
    brew install --cask dotnet-sdk

    # Check BaGet server availability
    curl -I http://10.0.2.2:5000/v3/index.json || echo "The BaGet server is unavailable"
  SHELL
end