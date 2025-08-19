---
title: ReceiptBox AI — 需求规格说明
date: 2025-08-05
author: 独立开发者
version: 1.0
---

## 架构

```mermaid
flowchart LR
  W[Web/PWA] --> N[Next.js]
  N --> A[.NET 8 API]
  A --> DB[(PostgreSQL)]
  A --> S3[对象存储]
  A --> LLM[GPT-4-turbo]
  A --> OCR[OCR 服务]
  N --> Pay[Stripe/Paddle]
```

## 核心接口

| 方法 | 路径 | 入参 | 出参 |
| --- | --- | --- | --- |
| POST | /api/receipt/ingest | file/emailId | receiptJson |
| POST | /api/budget/alert | thresholds | status |
| GET | /api/export | format | file |

## 数据表

| 表 | 字段 | 说明 |
| --- | --- | --- |
| receipts | id, userId, merchant, items, tax, currency | 票据主体 |
| budgets | id, userId, category, limit | 预算 |
| exports | id, userId, type, uri | 导出记录 |