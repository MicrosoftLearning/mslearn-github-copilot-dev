# Python Version of Library Management System

This folder contains a Python implementation of the C# library management system. The structure mirrors the C# solution, using Python best practices. Each major C# project is represented as a Python package:

- `application_core`: Domain models, interfaces, and business logic
- `infrastructure`: Data access (JSON-based)
- `console`: Console UI
- `tests`: Unit tests

To run the app:

```sh
cd AccelerateDevGHCopilotPython/console
python main.py
# search for "Twenty"
```

To run the tests:

```sh
cd AccelerateDevGHCopilotPython
python -m unittest discover -s tests
```
