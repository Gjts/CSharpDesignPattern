---
title: UXCopy Genie — 需求规格说明
date: 2025-08-05
author: 独立开发者
version: 1.0
---

## 架构

```mermaid
flowchart LR
  F[Figma 插件] --> N[Next.js BFF]
  N --> API[.NET 8 API]
  API --> DB[(PostgreSQL)]
  API --> LLM[GPT-4-turbo]
  N --> Pay[Stripe/Paddle]
```

## 接口

| 方法 | 路径 | 描述 |
| --- | --- | --- |
| POST | /api/copy/generate | 生成候选文案 |
| POST | /api/copy/ab-score | 评分与指标 |
| POST | /api/i18n/translate | 本地化 |

## 数据

| 表 | 字段 |
| --- | --- |
| templates | id, name, category, content |
| generations | id, userId, context, variants |
| experiments | id, variantA, variantB, result |