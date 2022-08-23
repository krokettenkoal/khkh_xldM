# Ghidra plus Eclipse <!-- omit in toc -->

TOC:

- [Deploy ghidra-emotionengine](#deploy-ghidra-emotionengine)
	- [Make ghidra-emotionengine an eclipse importable project](#make-ghidra-emotionengine-an-eclipse-importable-project)
- [Prepare Ghidra 9.1.2](#prepare-ghidra-912)
- [Install Eclipse](#install-eclipse)
- [Launch Eclipse](#launch-eclipse)
	- [Importing project](#importing-project)
	- [Install GhidraDev](#install-ghidradev)
	- [Java Build Path](#java-build-path)
	- [Link Ghidra](#link-ghidra)
	- [Launch Ghidra with extension loaded](#launch-ghidra-with-extension-loaded)
	- [Activate Gradle](#activate-gradle)
	- [Remove Gradle integration](#remove-gradle-integration)

# Deploy ghidra-emotionengine

If you want to use ghidra-emotionengine extension immediately, you can download it from releases:

- https://github.com/beardypig/ghidra-emotionengine/releases

If you want to use it with Eclipse, git clone:

- https://github.com/beardypig/ghidra-emotionengine

## Make ghidra-emotionengine an eclipse importable project

At least `.project` file is required to make it importable by Eclipse.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<projectDescription>
	<name>ghidra_emotionengine</name>
	<comment></comment>
	<projects>
	</projects>
	<buildSpec>
		<buildCommand>
			<name>org.eclipse.jdt.core.javabuilder</name>
			<arguments>
			</arguments>
		</buildCommand>
	</buildSpec>
	<natures>
		<nature>org.eclipse.jdt.core.javanature</nature>
	</natures>
</projectDescription>
```

_Note:_ `.project` file can be saved by entering `".project"` on save file dialog.

# Prepare Ghidra 9.1.2

I recommend to use `Ghidra 9.1.2`, not `9.2.x`. But still you can use favorite version.

Download it from https://ghidra-sre.org/releaseNotes_9.2.1.html or somewhere.

I have installed to `H:\Dev\KH2\ghidra_9.1.2_PUBLIC`

# Install Eclipse

Install Eclipse from https://www.eclipse.org/downloads/

`Eclipse IDE for Java Delopvers` will be enough selection.

![](images/2020-12-27_17h11_33.png)

Install it somewhere.

![](images/2020-12-27_17h11_46.png)

Launch it.

![](images/2020-12-27_17h15_11.png)

Specify `Workspace` folder as you like.
The new one is always created if it does not exists.
I recommend to allocate one dedicated workspace for KH2 stuffs.

![](images/2020-12-27_17h15_50.png)

# Launch Eclipse

Close `Welcome`

![](images/2020-12-27_17h30_51.png)

## Importing project

Click `Import projects...`

![](images/2020-12-27_17h31_36.png)

`Existing Projects into Workspace`

![](images/2020-12-27_17h40_00.png)

Enter `Select root directory:` path to ghidra-emotionengine.

![](images/2020-12-27_17h42_13.png)

Press enter will recognize the project. Click `Finish`.

![](images/2020-12-27_17h43_17.png)

We are now back to Eclipse IDE.

![](images/2020-12-27_17h43_56.png)

## Install GhidraDev

See: https://ghidra-sre.org/InstallationGuide.html#GhidraExtensionNotes

Also see: `<GhidraInstallDir>/Extensions/Eclipse/GhidraDev/GhidraDev_README.html`

Select: `Help` → `Install New Software...`

![](images/2020-12-27_17h52_22.png)

Click `Manage...`

![](images/2020-12-27_17h53_23.png)

Click `Add...`

![](images/2020-12-27_17h53_43.png)

Click `Archive...` and select `GhidraDev-2.1.0.zip`. And `Add`.

![](images/2020-12-27_17h54_24.png)

Click `Apply and Close`.

![](images/2020-12-27_17h55_26.png)

Select added `GhidraDev-2.1.0.zip` from `Work with:` drop down.

![](images/2020-12-27_17h56_07.png)

Turn on `Ghidra` in list, and then `Next >`.

![](images/2020-12-27_17h56_51.png)

`Next >` again.

![](images/2020-12-27_17h57_52.png)

Accept and finish.

![](images/2020-12-27_17h58_15.png)

`Install anyway`

![](images/2020-12-27_17h58_40.png)

`Restart Now`

![](images/2020-12-27_17h59_06.png)

`Yes`

![](images/2020-12-27_17h59_39.png)

Now we have `GhidraDev` pull down menu.

![](images/2020-12-27_18h00_30.png)

## Java Build Path

Opening ghidra_emotionengine project in _Package Explorer_ will show some contents.

![](images/2020-12-27_17h45_06.png)

To shorten the steps, place `.classpath` file in root of project. It is same folder having `.project` file.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<classpath>
	<classpathentry kind="src" path="src/main/java"/>
	<classpathentry kind="src" path="src/main/help"/>
	<classpathentry kind="src" path="src/main/resources"/>
	<classpathentry kind="src" path="ghidra_scripts"/>
	<classpathentry kind="output" path="bin"/>
</classpath>
```

Type `F5` will setup Java Build Path.

![](images/2020-12-27_17h48_01.png)

If you have plan change Java Build Path in GUI, right click `ghidra_emotionengine` project, and then click `Properties`.

Properties will open. Select `Java Build Path` tree item, and adjust as you like.

![](images/2020-12-27_18h02_13.png)

## Link Ghidra

Select `GhidraDev` → `Link Ghidra...`

![](images/2020-12-27_18h04_09.png)

`+`

![](images/2020-12-27_18h04_38.png)

`Add...`

![](images/2020-12-27_18h04_53.png)

Select `ghidra_9.1.2_PUBLIC` install dir.

`Apply and Close`

![](images/2020-12-27_18h05_45.png)

`Finish`

![](images/2020-12-27_18h06_29.png)

We get back to Eclipse IDE. And then all errors should be gone.

![](images/2020-12-27_18h06_56.png)

## Launch Ghidra with extension loaded

Click `Run As...`

![](images/2020-12-27_18h08_25.png)

`Ghidra` and `OK`

![](images/2020-12-27_18h09_20.png)

Your Ghidra will launch.

![](images/2020-12-27_18h15_42.png)

If you have already installed `ghidra-emotionengine` extension, collision error will be displayed.

![](images/2020-12-27_18h11_37.png)

- Disable `ghidra-emotionengine` extension while you use Ghidra on Eclipse.
- Enable it again if you launch Ghidra standalone.

## Activate Gradle

RClick project → `Configure` → `Add Gradle Nature`

![](images/2020-12-27_18h24_52.png)

RClick project → `Properties` → `Gradle` tree item

Add `-PGHIDRA_INSTALL_DIR=H:\Dev\KH2\ghidra_9.1.2_PUBLIC` or such to Program Arguments. `Apply and Close`

![](images/2020-12-27_18h27_51.png)

RClick project → `Gradle` → `Refresh Gradle Project`

![](images/2020-12-27_18h28_56.png)

If you have Gradle specific errors, you can select Gradle distribution:

![](images/2020-12-27_18h31_17.png)

## Remove Gradle integration

If you want to remove Gradle integration from project in some reasons, remove gradle things from `.project`. And then press `F5` at Eclipse.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<projectDescription>
	<name>ghidra-kh2ai-gh</name>
	<comment></comment>
	<projects>
	</projects>
	<buildSpec>
		<buildCommand>
			<name>org.eclipse.jdt.core.javabuilder</name>
			<arguments>
			</arguments>
		</buildCommand>
	</buildSpec>
	<natures>
		<nature>org.eclipse.jdt.core.javanature</nature>
	</natures>
</projectDescription>
```
