# RevitAddin.UpdaterTester

Revit Addin to show what `BuiltInParameter` is trigger using `IUpdater` on each `BuiltInParameter`.

[![Revit 2019](https://img.shields.io/badge/Revit-2019+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

## Video

[![VideoIma]][Video]

## Release

* [Latest release](../../releases/latest)

### AppBundleTool

The [ricaun.AppBundleTool](https://github.com/ricaun-io/ricaun.AppBundleTool) can be used to install/uninstall the `RevitAddin.UpdaterTester.bundle` by downloading it from the latest release.

#### Install
```bash
AppBundleTool -i -a https://github.com/ricaun-io/RevitAddin.UpdaterTester/releases/latest/download/RevitAddin.UpdaterTester.bundle.zip
```
#### Uninstall
```bash
AppBundleTool -u -a RevitAddin.UpdaterTester
```

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!

[Video]: https://youtu.be/Mg_2_C8w-LM
[VideoIma]: https://img.youtube.com/vi/Mg_2_C8w-LM/mqdefault.jpg