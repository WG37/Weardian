# Weardian

Weardian is a Zero-knowledge application for encrypting passwords into secure keys to be stored either locally, remotely or both. Encryption happens on client-side, and when synced sends wrapped keys to the server to store remotely. The server is "dumb" and has no knowledge of the keys whatsoever meaning they are purely stored. The zero-knowledge approach ensures that even in the event of a breach, sensitive information remains secure.


## Current Features

### Client-Side

- Encrypts plaintext passwords using AES-GCM (authenticated encryption)
- Implements key wrapping with AES-GCM for secure key handling
- Protects local keys and wrapped keys using DPAPI
- Optional encrypted synchronization with server
- Sync process transmits only wrapped (encrypted) keys
- Atomic file writes to prevent data corruption on failure

### Server-Side

- ASP.NET backend with RESTful APIs
- JWT-based authentication via ASP.NET Identity
- SQL-based persistence for encrypted data
- Stores only wrapped (encrypted) keys (zero-knowledge design)

## To be implemented

- Client & server-side logging for events, errors, and diagnostics
- WebView2 desktop shell with a React-based UI
- Support for storing different key types
- Support for storing and encrypting different data types
- Support for asymmetric encryption algorithms
- Additional symmetric encryption algorithm options
