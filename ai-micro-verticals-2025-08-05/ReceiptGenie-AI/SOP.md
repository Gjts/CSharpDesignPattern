---
title: ReceiptGenie AI — 研发与运营 SOP
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 研发

- 小票模板与字段规则以 YAML 管理；OCR 置信度低时触发人工确认。

## 部署

| 环节 | 工具 |
|---|---|
| 前端 | Cloudflare Pages |
| 后端 | Railway |
| DB | PostgreSQL |

## 监控

- 指标：识别成功率、导出成功率、退回率、转化。

## 支付与退款

- Stripe 主；2 天无条件退款。

## 插件 SOP

- Shopify：CLI + GraphQL Admin API；审核 1–3 天；分成 0%。
- Gmail：Apps Script Card Service + Gmail API；审核 3–7 天；分成 0%。

