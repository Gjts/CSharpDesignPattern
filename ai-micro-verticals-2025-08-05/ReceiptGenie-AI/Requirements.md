---
title: ReceiptGenie AI — 需求规格说明（Requirements）
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 范围与目标

- 自动抓取消费记录 → 生成合规小票/摘要 → 导出与报税简表。

## 用户旅程

| 步骤 | 行为 | 产出 |
|---|---|---|
| 抓取 | 邮箱/短信/照片 OCR | 原始记录 |
| 生成 | 模板/规则修复 | 合规小票/摘要 |
| 导出 | PDF/CSV/Notion | 报销材料 |

## 功能需求

- [ ] 邮箱/短信抓取与正则解析
- [ ] OCR 识别与修复建议
- [ ] 多币种换算与税率映射
- [ ] 导出与归档
- [ ] Shopify/Gmail 插件
- [ ] 支付/退款

## 非功能

- 准确率：关键字段识别 ≥ 97%。
- 隐私：本地脱敏；不长期留存图片原件（默认 30 天）。

## 外部集成

| 集成 | 方式 | 审核 | 分成 |
|---|---|---|---|
| Shopify | GraphQL Admin API | 1–3 天 | 0% |
| Gmail | Apps Script + Gmail API | 3–7 天 | 0% |
| Stripe/Paddle | Checkout + Webhook | 即时/1–2 天 | - |

