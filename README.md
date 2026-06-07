# ViewUnifier
ViewUnifier is an interactive console application that allows you to merge multiple UTF-8 encoded text files into a single file.

## Features

- Merge multiple text files into one output file
- UTF-8 encoding support
- Simple interactive command-line interface
- Lightweight and easy to use

## Requirements

- Windows

## Usage

Run the application:

```bash
ViewUnifier.exe
```

### Step 1 - Enter the output file name

Enter the name of the file that will contain the merged content.

Example:

```text
Output file name: merged.txt
```

### Step 2 - Enter input file paths

Type the path of each text file you want to merge.

Example:

```text
Path 1: .\directory1\
Path 2: .\directory2\file1.txt
Path 3:
```

When you are finished, press **Enter** on an empty line.

### Step 3 - Await

The application will:

1. Read all provided files.
2. Process them using UTF-8 encoding.
3. Merge their contents.
4. Save the result to the specified output file.

## 📄 License

This project is licensed under the MIT License.  
See the [LICENSE](./LICENSE) file for more details.