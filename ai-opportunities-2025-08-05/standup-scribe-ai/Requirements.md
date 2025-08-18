---
title: Standup Scribe AI — 需求规格说明
date: 2025-08-05
author: 独立开发者
version: 1.0
---

## 架构

```mermaid
flowchart LR
  Slack --> N[Next.js]
  GitHub --> N
  Calendar --> N
  N --> A[.NET 8 API]
  A --> DB[(PostgreSQL)]
  A --> LLM[GPT-4-turbo]
  N --> Pay[Stripe/Paddle]
```

## 接口

| 方法 | 路径 | 描述 |
| --- | --- | --- |
| POST | /api/ingest/slack | 事件接收 |
| GET | /api/report/daily | 生成日报 |
| GET | /api/report/weekly | 生成周报 |

## 数据

| 表 | 字段 |
| --- | --- |
| sources | id, type, payload |
| reports | id, userId, type, content |
| schedules | id, userId, cron, channels |