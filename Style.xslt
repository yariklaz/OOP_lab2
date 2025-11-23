<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"/>

	<xsl:template match="/">
		<html>
			<head>
				<title>Успішність студентів</title>
				<style>
					table { border-collapse: collapse; width: 100%; }
					th, td { border: 1px solid black; padding: 8px; text-align: left; }
					th { background-color: #f2f2f2; }
				</style>
			</head>
			<body>
				<h2>Звіт успішності</h2>
				<table>
					<tr>
						<th>Факультет</th>
						<th>Кафедра</th>
						<th>Студент</th>
						<th>Предмет</th>
						<th>Оцінка</th>
					</tr>
					<xsl:for-each select="University/Faculty/Department/Student/Session/Subject">
						<tr>
							<td>
								<xsl:value-of select="../../../../@Name"/>
							</td>
							<td>
								<xsl:value-of select="../../../@Name"/>
							</td>
							<td>
								<xsl:value-of select="../../@Name"/>
							</td>
							<td>
								<xsl:value-of select="@Title"/>
							</td>
							<td>
								<xsl:value-of select="@Grade"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
