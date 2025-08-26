---
title: ToneBridge AI — 需求规格说明（Requirements）
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 目标与范围

- 一键跨文化语气本地化与结构优化；支持 EN/JP/KR/ES/DE/CN。
- Gmail 与 Outlook 插件提供原地改写。

## 用户旅程

| 步骤 | 行为 | 产出 |
|---|---|---|
| 粘贴文本 | 选择目标文化/语气 | 生成 3 个候选 |
| 审阅差异 | 锁定事实、接受/拒绝 | 最终版本 |
| 模板保存 | 加入团队库 | 场景复用 |

## 功能需求

- [ ] 语气预设与文化词典（可配置）
- [ ] 事实保护（URL/金额/日期）
- [ ] 差异高亮与一键替换
- [ ] 模板库与分享
- [ ] Gmail/Outlook 插件：侧栏与上下文菜单
- [ ] 计费与额度（公平使用）

## 非功能需求

- 性能：单次改写 ≤ 3 秒；P95 ≤ 6 秒。
- 隐私：不训练用户文本；可一键清除历史。

## 数据模型

| 实体 | 字段 |
|---|---|
| User | id,email,plan |
| Rewrite | id,userId,locale,tone,latency |
| Template | id,industry,locale,body |

## 外部集成

| 集成 | 方式 | 审核 | 分成 |
|---|---|---|---|
| Gmail Add-on | Apps Script Card Service + Gmail API | 3–7 天 | 0% |
| Outlook Add-in | Office JavaScript API（Outlook） | 3–5 天 | 0% |
| Stripe/Paddle | Checkout + Webhook | 即时/1–2 天 | - |

## 验收

| 项 | 标准 |
|---|---|
| 正确性 | 事实不变更；敏感词不过界 |
| 性能 | P95 ≤ 6 秒 |
| 稳定性 | 错误率 ≤ 1%/日 |

## 里程碑（30 天）

- [ ] 词典与预设（D1–D5）
- [ ] 插件 MVP（D6–D14）
- [ ] 模板与团队（D15–D22）
- [ ] 支付与指标（D23–D30）

