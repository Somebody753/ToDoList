## Project Overview

This application allows authenticated users to create and join groups.  
Each group contains a shared task list and a real-time chat, making it a simple collaboration tool.

## Features

- User authentication and authorization using **ASP.NET Identity**
- Group management (create, join, leave group)
- Group-based task management
  - Tasks have deadlines that dynamically change their visual status
  - Tasks can be marked as completed, which also updates their status
- Real-time group chat created with **SignalR**
  - Messages are delivered in real time
  - Recently sent messages (from the last X minutes) are loaded when a user joins the chat

## Technical Details

- Built with **ASP.NET MVC**
- **SignalR** used for real-time communication
- **ASP.NET Identity** extended to support:
  - User–Message relationship (1:N)
  - User–Group relationship (N:M)
- Relational database with multiple tables and relationships
  - Includes a junction table for many-to-many relationships

## Purpose

This project was created as a portfolio application to demonstrate:
- Backend web development fundamentals
- Working with relational databases and entity relationships
- Real-time communication
- Authentication and authorization in ASP.NET applications
