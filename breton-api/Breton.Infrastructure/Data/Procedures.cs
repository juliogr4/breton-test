namespace Breton.Infrastructure.Data;

public static class Procedures
{
    // user
    public static readonly string pr_user_create = "pr_user_create";
    public static readonly string pr_user_get_by_email = "pr_user_get_by_email";
    public static readonly string pr_user_get_by_email_token = "pr_user_get_by_email_token";
    public static readonly string pr_user_email_confirmation = "pr_user_email_confirmation";

    // customer
    public static readonly string pr_customer_create = "pr_customer_create";
    public static readonly string pr_customer_delete = "pr_customer_delete";
    public static readonly string pr_customer_get = "pr_customer_get";
    public static readonly string pr_customer_get_by_id = "pr_customer_get_by_id";
    public static readonly string pr_customer_update = "pr_customer_update";
    public static readonly string pr_customer_get_by_cpf = "pr_customer_get_by_cpf";
}