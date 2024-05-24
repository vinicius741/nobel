CREATE PROCEDURE GetFilteredTransactions (
    @CampaignId INT = NULL,
    @AgentId INT = NULL,
    @Finalid INT = NULL
)
AS
BEGIN
    SELECT 
        C.NOMBRE AS CampaignName,
        T.idCampanya AS CampaignID,
        U.NOMBRE AS AgentName,
        T.idAgente AS AgentID,
        U.LOGIN AS AgentLogin,
        COUNT(T.idTransaccion) AS TotalTransactions,
        SUM(T.monto) AS TotalAmount
    FROM 
        TRANSACCION T
    JOIN 
        USUARIO U ON T.idAgente = U.idUsuario
    JOIN 
        CAMPANYA C ON T.idCampanya = C.idCampanya
    WHERE 
        (@CampaignId IS NULL OR T.idCampanya = @CampaignId) AND
        (@AgentId IS NULL OR T.idAgente = @AgentId) AND
        (@Finalid IS NULL OR T.idFinal = @Finalid)
    GROUP BY 
        C.NOMBRE,
        T.idCampanya,
        U.NOMBRE,
        T.idAgente,
        U.LOGIN
    ORDER BY 
        C.NOMBRE, U.NOMBRE;
END;
