# Client application

**Warning**: Ensure the server is started first, then start the client.

## Protocol

TCP Commands supported:

 1. **Generate Codes** 
- `generate <count> <length>`  
  Example: `generate 10 8`

 2. **Use Codes** 
- `use <code>`  
  Example: `use ABC12345`