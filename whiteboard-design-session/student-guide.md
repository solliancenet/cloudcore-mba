# CloudCore MBA workshop whiteboard design session student guide

October 2019

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2019 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**:

- [CloudCore MBA workshop whiteboard design session student guide](#cloudcore-mba-workshop-whiteboard-design-session-student-guide)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Step 1: Review the customer case study](#step-1-review-the-customer-case-study)
    - [Customer situation](#customer-situation)
    - [Customer needs](#customer-needs)
    - [Customer objections](#customer-objections)
    - [Infographic for common scenarios](#infographic-for-common-scenarios)
  - [Step 2: Design a proof of concept solution](#step-2-design-a-proof-of-concept-solution)
    - [Business needs](#business-needs)
    - [Design](#design)
    - [Address customer objections](#address-customer-objections)
  - [Step 3: Present the solution](#step-3-present-the-solution)
  - [Additional references](#additional-references)

## Abstract and learning objectives

In this whiteboard design session, you will work with a group to analyze customer requirements and design a solution to modernize the customer's e-commerce website and backend services. The goal is to use Azure platform-as-a-service (PaaS) services for the public-facing and backend websites while providing a way for the on-premises components to communicate with these services securely. You will also design fault-tolerance and a regional failover plan of the Azure components.

By the end of this whiteboard design session, you will have a better understanding of the key processes used to architect Azure solutions from a given set of customer requirements, with a focus on delivering on the Azure value proposition.

## Step 1: Review the customer case study

**Outcome**

Analyze your customer's needs.

Timeframe: 15 minutes

Directions: With all participants in the session, the facilitator/SME presents an overview of the customer case study along with technical tips.

1. Meet your table participants and trainer.
2. Read all of the directions for steps 1-3 in the student guide.
3. As a table team, review the following customer case study.

### Customer situation

The Contoso Sports League Association (CSLA) is one of the world's largest sports franchises. They have over 100 championships in their history and a vast, passionate fan base. They run a highly successful e-commerce website that sells merchandise to their legions of sports fans. The site is built using ASP.NET and hosted in a co-location facility near their corporate headquarters.

They accept payment by credit card and owing to their high annual volume (in the tens of millions, processing about 50K per day) of transactions, need to ensure that they are Payment Card Industry Data Security Standards (PCI DSS) Level 1 compliant. Their website hosts the shopping cart and checkout process, but they defer the credit card authorization and capture responsibilities to a third-party payment gateway. This payment gateway provides a web application programming interface (API) that is invoked over Transport Layer Security (TLS) from within their web application. The API call includes the credit cardholder data (name, number, and so on) and returns a status indicating success or failure in authorizing and capturing payment against the credit card. It is called after the customer clicks checkout, as a part of processing the order. They currently store their customer and profile data in SQL Server 2014.

In addition to the public-facing e-commerce website, they have a backend website that supports their call center. Call center employees use this admin website to view customer orders. Customers can call into the call center to place orders and pay for orders with their credit cards by phone.

They have reached a point where managing their server infrastructure is becoming a real challenge. Contoso wants to understand more about platform-as-a-service (PaaS) solutions available in Azure. They wonder if PaaS could help them focus their efforts more on the core business value rather than infrastructure. They have observed that Azure has received PCI compliance certification and are interested in moving their solution to Azure. "We're finding that with every upgrade, we're spending more and more engineering time on infrastructure and less on the experience that matters most to our fan base," says Miles Strom, Chief Executive Officer (CEO) of Contoso Sports League Association, "we need to rebalance those efforts."

One example is in how they manage the usernames and passwords for call center operators and support staff, as applied to the call center admin website. Today they have a homegrown solution that stores usernames and passwords in the same database used for storing merchandise information. They have experimented with other third-party solutions in the past. Their employees found it jarring to see another company's logo displayed when logging into their internal call center website. In creating their identity solution, they want to ensure they can brand the login screens with their logo. Additionally, Contoso is concerned about hackers from foreign countries/regions gaining access to the administrator site. Before they choose an identity solution, they would like to see how it indicates such attempts.

There is one architectural enhancement Contoso would like to make in the transition to a PaaS solution. When a visitor loads the home page, it gets the list of featured products on offer (consisting of the product image, title, and URL) from the Offers service. The home page does it using a client-side GET request against an ASP.NET Web API 2 service that is executed as the page loads in the browser. Contoso anticipates growing the functionality of this service and would like to scale it independently of the website.

### Customer needs

1. Implement a solution architecture that helps to minimize infrastructure management through the use of PaaS service offerings.

2. Ensure data privacy and protection across all aspects of the system, in transit and at rest.

3. Provide a better solution for the management of usernames and passwords.

4. Provide a regional database failover plan that will automatically initiate the failover to another region, allowing their various web applications and other hosted services to roll over to a synchronized database.

5. Want to take advantage of serverless compute resources available in Azure to enhance their existing e-commerce site.

### Customer objections

1. It is not clear to us from the Azure Trust Center how Azure helps our solution become PCI compliant.

2. Can we provide a solution that scales to meet our public demand, but is also secure for use by our call center and warehouse?

3. Our PCI compliance requires us to have a quarterly audit and to conduct occasional penetration tests. Does Azure support it?

4. Our previous infrastructure did not have adequate performance monitoring of our websites. What options would you recommend we investigate that would work with our web apps in Azure?

5. Is it possible to automatically failover the database without having to update connection settings in our applications?

### Infographic for common scenarios

![This diagram is of a Common scenario for an E-Commerce Website. The diagram begins with an end-user, includes a services tier, internet tier, and data tier, and ends at an Enterprise. The diagram also includes Microsoft Azure and Azure Virtual Network.](media/image2.png "Common scenario for an E-Commerce Website")

## Step 2: Design a proof of concept solution

**Outcome**

Design a solution and prepare to present the solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 60 minutes

### Business needs

Directions:  With all participants at your table, answer the following questions and list the answers on a flip chart:

1. Who should you present this solution to? Who is your target customer audience? Who are the decision makers?
2. What customer business needs do you need to address with your solution?

### Design

Directions: With all participants at your table, respond to the following questions on a flip chart:

*High-level architecture*

Microsoft CSAs have provided the following high-level solution architecture diagram. Take some time to review the diagram and associated description before moving on to the questions below.

![A diagram that depicts the various Azure PaaS services for the solution. Azure AD Org is used for authentication to the call center app. Azure AD B2C for authentication is used for authentication to the client app. SQL Database for the backend customer data. Azure App Services for the web and API apps. Order processing includes using Functions, Logic Apps, Queues, and Storage. Azure App Insights provides telemetry capture capabilities.](media/solution-architecture.png "Solution Architecture Diagram")

From a high-level, the customer's e-commerce and call center websites are hosted using Web Apps. The App Services are inside of an App Service Environment, which provides the necessary controls to ensure PCI compliance. Each of the public websites is secured using Azure Active Directory (Azure AD). The Offers and Payment Gateway APIs are running in API Apps. Order processing is implemented using various serverless technologies, including Logic Apps, Azure Functions, Azure Storage Queues, and Azure Blob storage. When visiting the e-commerce website, customers are presented with offers that are served from the Offers REST API hosted within an API App. Orders are submitted by customers via the e-commerce website. Credit card validation is part of the checkout process and uses a third-party payment gateway. Once authorized and payment is captured, the order data is written to the orders Azure SQL Database, and the order details are sent to a processing queue. The Logic App is trigger by items being added to the queue. The Logic App triggers an Azure Function to create PDF receipts for customer purchases. Customers are notified via SMS as their order is processed using the [Twilio](https://www.twilio.com/) connector integrated into the Logic App.

> **Note**: The above solution is only one of many possible, viable approaches.

1. Can you identify the PaaS services used in the proposed solution? Are there any IaaS services? Are there any serverless components? Why do you think the proposed architecture favors PaaS over IaaS for this customer?

*Notifications*

1. Based on the proposed solution architecture, what services are being recommended for CSLA to manage notifying customers that their order has been processed? Are these IaaS, PaaS, or SaaS services?

*Offers service*

1. How are offers being served to the e-commerce website? What service is being used to enable this functionality?

*Geo-resiliency*

1. What is meant by high availability?

2. How does the solution implement high availability for the orders database to guard against regional data center outages?

*Access control*

1. Access control is authentication and authorization. What does each of those mean?

2. For managing access to the call center website, explain how the proposed solution handles meeting Contoso's requirements around authentication and authorization.

*Enabling PCI compliance*

1. Broadly speaking, what does it mean for a solution to be PCI compliant?

2. To maintain PCI compliance for the e-commerce website, the proposed solution recommends deploying the Web App into an Azure App Service Environment. The CSA explained that the purpose of using an ASE is to restrict where data can go from the website. Why do you think this helps to make the solution PCI compliant?

### Address customer objections

As a team, ensure you have addressed all of the customer objections listed above.

**Prepare**

Directions: With all participants at your table:

1. Identify any customer needs that are not addressed with the proposed solution.
2. Identify the benefits of your solution.
3. Determine how you will respond to the customer's objections.

Prepare a 15-minute chalk-talk style presentation to the customer.

## Step 3: Present the solution

**Outcome**

Present a solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 30 minutes

**Presentation**

Directions:

1. Pair with another table.
2. One table is the Microsoft team and the other table is the customer.
3. The Microsoft team presents their proposed solution to the customer.
4. The customer makes one of the objections from the list of objections.
5. The Microsoft team responds to the objection.
6. The customer team gives feedback to the Microsoft team.
7. Tables switch roles and repeat Steps 2-6.

## Additional references

|                                                              |                                                                                                                             |
| ------------------------------------------------------------ | -------------------------------------------------------------------------------------------------------------------------- |
| **Description**                                              | **Links**                                                                                                                   |
| Compliance Commitments                                       | <http://azure.microsoft.com/en-us/support/trust-center/services/>                                                           |
| Azure App Services                                           | <https://azure.microsoft.com/en-us/documentation/articles/app-service-value-prop-what-is/>                                  |
| Azure Service Environment (ASE)                              | <https://azure.microsoft.com/en-us/documentation/articles/app-service-app-service-environment-intro/>                       |
| Integrate ILB ASE with Azure Application Gateway             | <https://docs.microsoft.com/en-us/azure/app-service/environment/integrate-with-application-gateway>                         |
| Configuring ASE Network Security Groups                      | <https://docs.microsoft.com/en-us/azure/app-service/environment/network-info#network-security-groups>                       |
| Geo Distributed Scale with ASE                               | <https://docs.microsoft.com/en-us/azure/app-service/environment/app-service-app-service-environment-geo-distributed-scale>  |
| Azure Trust Center                                           | <http://azure.microsoft.com/en-us/support/trust-center/>                                                                    |
| Azure PCI Attestation of Compliance                          | <http://download.microsoft.com/download/7/1/E/71E02A19-D1A4-448F-8CEA-D6A19398ABDA/Azure%20PCI%20AOC%20Feb%202015.pdf>      |
| PCI DSS v3.0                                                 | <https://www.pcisecuritystandards.org/documents/PCI_DSS_v3.pdf>                                                             |
| Azure Data Factory                                           | <https://azure.microsoft.com/en-us/documentation/articles/data-factory-data-movement-activities/#data-factory-copy-wizard/> |
| Azure SQL Database                                           | <https://docs.microsoft.com/en-us/azure/sql-database/sql-database-geo-replication-overview/>                                |
| Designing highly available services using Azure SQL Database | <https://docs.microsoft.com/en-us/azure/sql-database/sql-database-designing-cloud-solutions-for-disaster-recovery>          |
